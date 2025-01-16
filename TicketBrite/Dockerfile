# Stage 1: Build
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Development
FROM node:18 AS dev
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .

# Start de development server
CMD ["npm", "run", "dev"]
EXPOSE 5173

# Stage 3: Production
FROM nginx:stable-alpine AS prod
# Kopieer de aangepaste nginx.conf naar de juiste locatie
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]