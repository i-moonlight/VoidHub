ARG NODE_VERSION=18.17.0

FROM node:${NODE_VERSION}-alpine

WORKDIR /usr/src/app
COPY package.json package-lock.json ./

RUN npm install
RUN npm install -g @angular/cli

ENV NODE_ENV production

COPY . .

EXPOSE 4200

CMD ng serve --host=0.0.0.0 --configuration production
