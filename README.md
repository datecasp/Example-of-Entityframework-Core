# Example-of-Entityframework-Core (WIP)
This project shows the use of Entity Framework Core as ORM 
for the manage of a database.

It represents the relation betwen users (Usuarios) and books
(Libros) and betwen these and categories (Categorias)

A user can have assigned or not a book and can have a collection
of books.

A book only has or not only one user (the actual who has the book)
and has a collection of past users (AntiguosUsuarios)
Also a book must have at least one category and can have a 
collection of them.

Categories must have at least one book and can have a collection
of them.

# Controllers
With the controllers you can execute the basic common operations
like add a user, a book or a category, modify one of them, get 
different results (like users, users of a bokk, categories of a
book, assign book to user and so on). All of them are CRUD operations
with customized controllers.

# Database
The database is managed with SSMS with SQLEXPRESS and using 
Linq for the queries.

# TODO:
+Add Services for reuse code and slim controllers

+Add autentication
