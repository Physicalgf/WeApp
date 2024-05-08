   # 使用官方Node.js镜像作为基础镜像
   FROM node:14-alpine AS build-stage
   WORKDIR /app
   COPY package*.json ./
   RUN npm install
   COPY . .
   RUN npm run build

   # 生产环境镜像
   FROM nginx:stable-alpine AS production-stage
   COPY --from=build-stage /app/dist /usr/share/nginx/html
   EXPOSE 80
   CMD ["nginx", "-g", "daemon off;"]