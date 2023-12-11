ARG NODE_VERSION=18.17.0

FROM node:${NODE_VERSION}-alpine

WORKDIR /usr/src/app
COPY package.json package-lock.json ./

RUN npm install
RUN npm install -g @angular/cli

COPY . .
COPY ./src/environments/environment.docker.ts ./src/environments/environment.ts

# My pc is like potato, so change parameter it depends on your pc :D
RUN export NODE_OPTIONS="--max-old-space-size=1024"

WORKDIR /usr/src/app/ckeditor5
RUN npm install
RUN npm run build

WORKDIR /usr/src/app
RUN npm install ./ckeditor5/

ENV NODE_ENV production

EXPOSE 4200

CMD ng serve --host=0.0.0.0 --disable-host-check --configuration production
