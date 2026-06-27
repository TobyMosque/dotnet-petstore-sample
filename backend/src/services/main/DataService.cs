using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using PetStore.Domain;
using PetStore.Domain.Entities;
using PetStore.Services.Abstractions.Services;
using RT.Comb;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Writers;

namespace PetStore.Services
{
    public class DataService : IDataService
    {
        private readonly PetStoreContext _db;
        private readonly ICombProvider _comb;

        public DataService(PetStoreContext db, ICombProvider comb)
        {
            _db = db;
            _comb = comb;
        }

        public async Task<string> GenerateAsync(CancellationToken ct)
        {
            await ClearAsync(ct);

            var categoryNames = new[] { "Dogs", "Cats", "Birds", "Reptiles", "Fish" };
            var tagNames = new[] { "young", "old", "male", "female", "vaccinated", "trained", "rescue" };

            var categories = categoryNames.Select(n => new Category { Id = _comb.Create(), Name = n }).ToList();
            var tags = tagNames.Select(n => new Tag { Id = _comb.Create(), Name = n }).ToList();

            await _db.Categories.AddRangeAsync(categories, ct);
            await _db.Tags.AddRangeAsync(tags, ct);
            await _db.SaveChangesAsync(ct);

            var categoryIds = categories.Select(c => c.Id).ToArray();
            var tagList = tags.ToArray();

            var f = new Faker();

            var pets = new List<Pet>();
            for (var i = 0; i < 50; i++)
            {
                var pet = new Pet
                {
                    Id = _comb.Create(),
                    Name = f.Name.FirstName(),
                    Status = f.PickRandom("available", "pending", "sold").OrNull(f, .25f),
                    CategoryId = f.PickRandom(categoryIds),
                    PhotoUrls = Enumerable.Range(0, f.Random.Int(1, 4))
                        .Select(_ => new PetPhoto { Id = _comb.Create(), Url = f.Internet.Url() })
                        .ToList()
                };
                pet.Tags = f.Random.ListItems(tagList, f.Random.Int(0, 3)).ToList();
                pets.Add(pet);
            }

            await _db.Pets.AddRangeAsync(pets, ct);
            await _db.SaveChangesAsync(ct);

            var petIds = pets.Select(p => (Guid?)p.Id).ToArray();

            var orders = new List<Order>();
            for (var i = 0; i < 30; i++)
            {
                orders.Add(new Order
                {
                    Id = _comb.Create(),
                    PetId = f.PickRandom(petIds),
                    Quantity = f.Random.Int(1, 10).OrNull(f, .25f),
                    ShipDate = DateTime.SpecifyKind(f.Date.Future(), DateTimeKind.Utc).OrNull(f, .25f),
                    Status = f.PickRandom("placed", "approved", "delivered").OrNull(f, .25f),
                    Complete = f.Random.Bool()
                });
            }

            await _db.Orders.AddRangeAsync(orders, ct);

            var users = new List<User>();
            for (var i = 0; i < 20; i++)
            {
                users.Add(new User
                {
                    Id = _comb.Create(),
                    Username = f.Internet.UserName() + "_" + i,
                    FirstName = f.Name.FirstName().OrNull(f, .25f),
                    LastName = f.Name.LastName().OrNull(f, .25f),
                    Email = f.Internet.Email().OrNull(f, .25f),
                    Password = f.Internet.Password(),
                    Phone = f.Phone.PhoneNumber().OrNull(f, .25f),
                    UserStatus = f.Random.Int(0, 2)
                });
            }

            await _db.Users.AddRangeAsync(users, ct);
            await _db.SaveChangesAsync(ct);

            return $"Generated {categories.Count} categories, {tags.Count} tags, {pets.Count} pets, {orders.Count} orders, {users.Count} users.";
        }

        public async Task<byte[]> ExportAsync(CancellationToken ct)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };

            var cats = await _db.Categories.ToListAsync(ct);
            var tags = await _db.Tags.ToListAsync(ct);
            var pets = await _db.Pets.Include(p => p.PhotoUrls).Include(p => p.Tags).ToListAsync(ct);
            var orders = await _db.Orders.ToListAsync(ct);
            var users = await _db.Users.ToListAsync(ct);

            var files = new Dictionary<string, byte[]>
            {
                ["categories.csv"] = ToCsv(cats.Select(c => new { c.Id, c.Name }), csvConfig),
                ["tags.csv"] = ToCsv(tags.Select(t => new { t.Id, t.Name }), csvConfig),
                ["pets.csv"] = ToCsv(pets.Select(p => new { p.Id, p.Name, p.Status, p.CategoryId }), csvConfig),
                ["pet_photos.csv"] = ToCsv(pets.SelectMany(p => p.PhotoUrls).Select(ph => new { ph.Id, ph.PetId, ph.Url }), csvConfig),
                ["pet_tags.csv"] = ToCsv(pets.SelectMany(p => p.Tags.Select(t => new { PetId = p.Id, TagId = t.Id })), csvConfig),
                ["orders.csv"] = ToCsv(orders.Select(o => new { o.Id, o.PetId, o.Quantity, o.ShipDate, o.Status, o.Complete }), csvConfig),
                ["users.csv"] = ToCsv(users.Select(u => new { u.Id, u.Username, u.FirstName, u.LastName, u.Email, u.Password, u.Phone, u.UserStatus }), csvConfig)
            };

            using var ms = new MemoryStream();
            using (var archive = ArchiveFactory.Create(ArchiveType.Zip))
            {
                foreach (var (name, data) in files)
                {
                    archive.AddEntry(name, new MemoryStream(data), false, data.Length);
                }
                archive.SaveTo(ms, new WriterOptions(CompressionType.Deflate));
            }
            return ms.ToArray();
        }

        public async Task<string> SeedAsync(CancellationToken ct)
        {
            var assembly = typeof(PetStoreContext).Assembly;
            var resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith("seed.zip"));

            if (resourceName == null)
                throw new InvalidOperationException("Embedded seed resource not found.");

            using var stream = assembly.GetManifestResourceStream(resourceName);
            return await ImportAsync(stream, ct);
        }

        public async Task<string> ImportAsync(Stream archive, CancellationToken ct)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };
            var extracted = new Dictionary<string, byte[]>();

            using (var sz = ArchiveFactory.Open(archive))
            {
                foreach (var entry in sz.Entries.Where(e => !e.IsDirectory))
                {
                    using var entryStream = entry.OpenEntryStream();
                    using var buf = new MemoryStream();
                    await entryStream.CopyToAsync(buf, ct);
                    extracted[entry.Key] = buf.ToArray();
                }
            }

            await ClearAsync(ct);

            if (extracted.TryGetValue("categories.csv", out var catBytes))
            {
                var rows = FromCsv<CategoryRow>(catBytes, csvConfig);
                var cats = rows.Select(r => new Category { Id = r.Id, Name = r.Name }).ToList();
                await _db.Categories.AddRangeAsync(cats, ct);
                await _db.SaveChangesAsync(ct);
            }

            if (extracted.TryGetValue("tags.csv", out var tagBytes))
            {
                var rows = FromCsv<TagRow>(tagBytes, csvConfig);
                var tags = rows.Select(r => new Tag { Id = r.Id, Name = r.Name }).ToList();
                await _db.Tags.AddRangeAsync(tags, ct);
                await _db.SaveChangesAsync(ct);
            }

            if (extracted.TryGetValue("pets.csv", out var petBytes))
            {
                var rows = FromCsv<PetRow>(petBytes, csvConfig);
                var pets = rows.Select(r => new Pet { Id = r.Id, Name = r.Name, Status = r.Status, CategoryId = r.CategoryId }).ToList();
                await _db.Pets.AddRangeAsync(pets, ct);
                await _db.SaveChangesAsync(ct);
            }

            if (extracted.TryGetValue("pet_photos.csv", out var photoBytes))
            {
                var rows = FromCsv<PetPhotoRow>(photoBytes, csvConfig);
                var photos = rows.Select(r => new PetPhoto { Id = r.Id, PetId = r.PetId, Url = r.Url }).ToList();
                await _db.PetPhotos.AddRangeAsync(photos, ct);
                await _db.SaveChangesAsync(ct);
            }

            if (extracted.TryGetValue("pet_tags.csv", out var petTagBytes))
            {
                var rows = FromCsv<PetTagRow>(petTagBytes, csvConfig);
                foreach (var row in rows)
                {
                    var pet = await _db.Pets.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == row.PetId, ct);
                    var tag = await _db.Tags.FindAsync(new object[] { row.TagId }, ct);
                    if (pet != null && tag != null)
                    {
                        pet.Tags.Add(tag);
                    }
                }
                await _db.SaveChangesAsync(ct);
            }

            if (extracted.TryGetValue("orders.csv", out var orderBytes))
            {
                var rows = FromCsv<OrderRow>(orderBytes, csvConfig);
                var orders = rows.Select(r => new Order { Id = r.Id, PetId = r.PetId, Quantity = r.Quantity, ShipDate = r.ShipDate, Status = r.Status, Complete = r.Complete }).ToList();
                await _db.Orders.AddRangeAsync(orders, ct);
                await _db.SaveChangesAsync(ct);
            }

            if (extracted.TryGetValue("users.csv", out var userBytes))
            {
                var rows = FromCsv<UserRow>(userBytes, csvConfig);
                var users = rows.Select(r => new User { Id = r.Id, Username = r.Username, FirstName = r.FirstName, LastName = r.LastName, Email = r.Email, Password = r.Password, Phone = r.Phone, UserStatus = r.UserStatus }).ToList();
                await _db.Users.AddRangeAsync(users, ct);
                await _db.SaveChangesAsync(ct);
            }

            return "Import complete.";
        }

        private async Task ClearAsync(CancellationToken ct)
        {
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM \"PetTag\"", ct);
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM \"PetPhotos\"", ct);
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM \"Pets\"", ct);
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM \"Orders\"", ct);
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM \"Users\"", ct);
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM \"Categories\"", ct);
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM \"Tags\"", ct);
        }

        private static byte[] ToCsv<T>(IEnumerable<T> records, CsvConfiguration config)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, Encoding.UTF8);
            using var csv = new CsvWriter(writer, config);
            csv.WriteRecords(records);
            writer.Flush();
            return ms.ToArray();
        }

        private static List<T> FromCsv<T>(byte[] data, CsvConfiguration config)
        {
            using var ms = new MemoryStream(data);
            using var reader = new StreamReader(ms, Encoding.UTF8);
            using var csv = new CsvReader(reader, config);
            return csv.GetRecords<T>().ToList();
        }

        private class CategoryRow { public Guid Id { get; set; } public string Name { get; set; } }
        private class TagRow { public Guid Id { get; set; } public string Name { get; set; } }
        private class PetRow { public Guid Id { get; set; } public string Name { get; set; } public string Status { get; set; } public Guid? CategoryId { get; set; } }
        private class PetPhotoRow { public Guid Id { get; set; } public Guid PetId { get; set; } public string Url { get; set; } }
        private class PetTagRow { public Guid PetId { get; set; } public Guid TagId { get; set; } }
        private class OrderRow { public Guid Id { get; set; } public Guid? PetId { get; set; } public int? Quantity { get; set; } public DateTime? ShipDate { get; set; } public string Status { get; set; } public bool Complete { get; set; } }
        private class UserRow { public Guid Id { get; set; } public string Username { get; set; } public string FirstName { get; set; } public string LastName { get; set; } public string Email { get; set; } public string Password { get; set; } public string Phone { get; set; } public int UserStatus { get; set; } }
    }
}
