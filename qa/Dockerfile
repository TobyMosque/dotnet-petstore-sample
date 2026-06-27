FROM node:lts-alpine AS base
RUN npm i --location=global pnpm@10 @quasar/cli@latest
WORKDIR /src

FROM base AS source-stage
COPY package.json pnpm-lock.yaml ./
RUN pnpm install --frozen-lockfile --ignore-scripts
COPY . .

FROM source-stage AS build-stage
ARG API_URL=http://localhost:5000
ENV API_URL=$API_URL
RUN quasar build

FROM nginx:alpine AS production-stage
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage /src/dist/spa /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
