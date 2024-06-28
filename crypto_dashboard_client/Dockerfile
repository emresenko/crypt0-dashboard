FROM cirrusci/flutter:stable AS build
WORKDIR /app
COPY . .
RUN flutter build web

FROM nginx:alpine
COPY --from=build /app/build/web /usr/share/nginx/html
