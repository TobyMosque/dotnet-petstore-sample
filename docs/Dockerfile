FROM node:lts-alpine AS base
RUN corepack enable && corepack prepare pnpm@10 --activate
WORKDIR /src

FROM base AS source-stage
COPY package.json pnpm-lock.yaml ./
RUN pnpm install --frozen-lockfile
COPY . .

FROM source-stage AS build-stage
RUN pnpm run build

FROM nginx:alpine AS production-stage
COPY --from=build-stage /src/.vitepress/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
