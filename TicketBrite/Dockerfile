FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Voor ontwikkeling, draai de Vite server op poort 5173
FROM node:18 AS dev
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .

# Start de development server
CMD ["npm", "run", "dev"]

EXPOSE 5173