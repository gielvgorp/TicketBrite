# Stap 1: Gebaseerd op een officiële Node.js image
FROM node:18 AS build

# Zet de werkmap
WORKDIR /app

# Kopieer de package.json en installeer de afhankelijkheden
COPY package.json package-lock.json ./
RUN npm install

# Kopieer de rest van de applicatie en build de frontend
COPY . .
RUN npm run build

# Stap 2: Serve de app met een statische server
FROM nginx:alpine

# Kopieer de build naar de juiste locatie in de nginx container
COPY --from=build /app/dist /usr/share/nginx/html

# Stel de poort in
EXPOSE 5173

# Start de nginx server
CMD ["nginx", "-g", "daemon off;"]
