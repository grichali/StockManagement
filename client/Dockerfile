# Use a Node image to build the Angular app
FROM node:18 as build-stage

# Set working directory and copy app files
WORKDIR /app
COPY . .

# Install dependencies and build the app
RUN npm install
RUN npm run build --prod

# Use an Nginx image to serve the app
FROM nginx:alpine

# Copy the built app from the first stage
COPY --from=build-stage /app/dist/browser /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
