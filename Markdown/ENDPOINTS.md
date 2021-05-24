# Endpoints de la API
La API cuenta con 4 controladores. Un controlador para el login que cuenta con 2 endpoints y 3 controladores que interactúan con tablas de la Base de Datos, los cuales cuentan con 6 endpoints cada uno. Para obtener información más detallada de alguno de los controladores, haga click en el nombre.

* [Login](./LOGIN_ENDPOINTS.md) 🔐
  + GET /api/Login
  + POST /api/Login

* [Employee](./EMPLOYEE_ENDPOINTS.md) 👷‍♂️
  + GET /api/Employee/{id}
  + POST /api/Employee
  + PUT /api/Employee/{id}
  + DELETE /api/Employee/{id}
  + GET /api/Employee/pages/{requestedPage}
  + GET /api/Employee

* [Product](./PRODUCT_ENDPOINTS.md) 🛒
  + GET /api/Product/{id}
  + POST /api/Product
  + PUT /api/Product/{id}
  + DELETE /api/Product/{id}
  + GET /api/Product/pages/{requestedPage}
  + GET /api/Product

* [Customer](./CUSTOMER_ENDPOINTS.md) 🙍‍♂️
  + GET /api/Customer/{id}
  + POST /api/Customer
  + PUT /api/Customer/{id}
  + DELETE /api/Customer/{id}
  + GET /api/Customer/pages/{requestedPage}
  + GET /api/Customer