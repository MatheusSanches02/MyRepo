Projeto: MyRepo

API de um sistema com catálogo de projetos. 

Projeto desenvolvido utilizando C# com .Net 6.0 e Dapper como micro orm, baseado no padrão repository onde temos a seguinte estrutura de pastas:

![image](https://user-images.githubusercontent.com/79661325/222037114-5b68470a-e588-49db-bd2c-14b01d8ceb91.png)

Visto que em Models são implementadas da entidades do projeto;
Em Repository temos a interface garantindo que o programa execute com os métodos corretos e implementando a interface, temos a classe repository,
cujo qual fará acesso ao banco de dados através da ORM;
Em Controllers, criamos um objeto de repository para acessarmos os métodos de acesso ao banco de dados.

A string de conexão com o banco de dados, fica configurada em appsettings.json

Em Program.cs, deve-se adicionar a injeção de dependências necessária, no caso, como temos os métodos que acessam o banco de dados em Repository, devemos 
adicionar em Program.cs a seguinte linha:

builder.Services.AddTransient<IReposRepository, ReposRepository>();

Identificando assim, que faremos injeção de dependências.

Para rodar o projeto, estou utilizando alguns pacotes do NuGet, esses são:

Dapper, no caso a ORM que estou utilizando
![image](https://user-images.githubusercontent.com/79661325/222038623-1ab3a955-f097-44e7-be67-39aa7c6185fa.png)

System.Data.SqlClient, para criarmos a conexão com o SQL Server
![image](https://user-images.githubusercontent.com/79661325/222038672-2ddef03b-9564-4070-8005-9ab9fe8a4c86.png)
