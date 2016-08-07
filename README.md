## Building


Нужен установленный postgres.

В постгресе должна быть создана база данных **give_future**

Должен быть пользователь постгреса с именем **give_future**,
паролем **1q2w3e4r** и полными правами на базу данных **give_future**

* `npm install`
* `bower install`
* `dotnet restore`
* `dotnet ef migrations list`
* `dotnet ef database update <последняя миграция в списке из предыдущей команды>`
* `gulp watch`
* `dotnet run`

Для того, чтобы заполнить базу данных, нужны файлы приютов и животных из предыдущего проекта

Команда для заполнения приютов:

`dotnet run --init-db 1 --file <путь к файлу приютов> --command shelters`

Команда для заполнения животных:

`dotnet run --init-db 1 --file <путь к файлу животных> --command animals`

Есть некоторая вероятность, что с пустой БД работать не будет.
