# Example-of-Entityframework-Core (WIP)

Ejemplo de uso de Entity Framework Core para la gestión de una base de datos.

Muestra el uso de características del framework para implementar todo el 

proceso desde el diseño (Code-first) de las diferentes tablas y sus relaciones,

el uso de LinQ para realizar consultas o la gestión de roles de usuario para 

securizar diferentes operaciones usando un JsonWebToken.

Desarrollado con C# en VisualStudio2022

# Estructura:

La aplicación muestra cuatro bloques de controladores.

Account -> Ofrece funcionalidad de Login para usuarios con acceso a la BD.

Estos usuarios se denominan "GrantedUser" y pueden tener rol de Administrador

o de Usuario. Si el rol del usuario es "Administrador" además tendrá acceso 

a una serie de herramientas de gestión de usuarios.

Categorias, Libros y Usuarios -> Estos controladores ofrecen funcionalidad 

de gestión sobre los tres elementos que vamos a manejar en la base de datos.

Se crean tablas para estas tres entidades y para sus relaciones, N:M en el 

caso de Libros y Categorias (un libro tiene muchas categorías y una categoría

muchos libros) y 1:N para Libros y Usuarios (un libro sólo lo puede tener un 

usuario que a su vez puede tener varios libros). También se crea una entidad 

AntiguosUsuarios para guardar los usuarios que han tenido y devuelto un libro.

Mantiene una relación N:M con Libros.

Para todos estos controladores existen interfaces y clases de Servicios que 

implmentan la lógica, desacoplando el código y facilitando así su mantenimiento.

La clase OutModels.cs alberga clases DTO de las diferentes entidades para su

uso como resultado a mostrar, limitando y controlando así el contenido expuesto.

