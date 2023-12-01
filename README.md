## Before start:
1. ```cd API/ForumAPI```

2. ```cp appsettings_example.json appsettings.json```

    > than fill appsettings.json with your data (connection string, secrets)

3. install dotnet sdk and dotnet ef

## Local

1. api:
    ```
    cd API/ForumAPI
    ```
    1.1 database
    ```
    dotnet ef database update
    ```
    1.2 app
    ```
    dotnet run
    ```

2. client

    2.1 build ckeditor5
    ```
    cd client/ckeditor5
    ```
    ```
    npm install
    ```
    ```
    npm run build
    ```
    2.2 angular app
    ```
    cd client
    ```
    ```
    npm install
    ```
    ```
    npm install ./ckeditor5
    ```
    ```
    ng serve
    ```

## Docker

1. compose
    ```
    cp compose_example.yml compose.yml
    ```
    > fill db password in compose.yml

    > fill client/src/environments/environment.docker.ts (your api url)

    ```
    docker compose up -d
    ```
2. database
    ```
    cd API/ForumAPI
    ```
    ```
    dotnet ef database update 
    ```