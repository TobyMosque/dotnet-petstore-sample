namespace PetStore.Services.Abstractions.Dtos
{
    public class ApiResponseDto
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
