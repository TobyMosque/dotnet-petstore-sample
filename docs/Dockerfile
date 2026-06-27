FROM node:lts-alpine AS base
WORKDIR /src

FROM base AS source-stage
COPY package.json package-lock.json ./
RUN npm ci
COPY . .

FROM source-stage AS build-stage
RUN npm run build

FROM nginx:alpine AS production-stage
COPY --from=build-stage /src/.vitepress/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
