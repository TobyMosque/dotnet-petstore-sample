FROM node:lts-alpine AS base
RUN corepack enable
WORKDIR /src

FROM base AS source-stage
COPY package.json yarn.lock ./
RUN yarn install --frozen-lockfile
COPY . .

FROM source-stage AS build-stage
RUN yarn build

FROM nginx:alpine AS production-stage
COPY --from=build-stage /src/.vitepress/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
