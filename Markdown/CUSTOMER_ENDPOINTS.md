# Customer 🙍‍♂️
El Controlador Customer se permite a un usuario autenticado interactuar con la table Customer de la Base de Datos Northwind.

## ✔ Get One Customer

**Descripción**: Devuelve un JSON con la información del cliente solicitado.

**URL**: /api/Customer/{id}

**Método  HTTP**: GET

**Parámetros del URL**:
* **id**: Id del cliente solicitado. Número entero positivo.

**Parámetros del Body**: Ninguno

**Formato de Respuesta Satisfactoria**: Status 200

```typescript
{
	"id": number,
	"company": string,
	"contactFullName": string,
	"contactPosition": string,
	"contactPhone": string
}
```

**Prueba**:

![Get One Customer Postman](./pictures/Customer/get_one_customer.png)

***

## ✔ Add Customer

**Descripción**: Agrega un registro de la tabla Customer con la información especificada en el Body. Devuelve un JSON con el ID registrado para el cliente.

**URL**: /api/Customer/{id}

**Método  HTTP**: POST

**Parámetros del URL**: Ninguno

**Parámetros del Body**:

```typescript
{
	"id": number,
  	  /*
      - ID del cliente.
	  - Entero de 5 números máximo.
      - Obligatorio.
      */

	"company": string,
      /*
      - Nombre de la compañía del cliente.
      - String de 40 caracteres máximo.
      - Obligatorio.
      */

	"contactFullName": string,
      /*
      - Nombre completo del cliente.
      - String de 30 caracteres máximo.
      - Opcional.
      */

	"contactPosition": string,
      /*
      - Puesto del cliente.
      - String de 30 caracteres máximo.
      - Opcional.
      */

	"contactPhone": string
	  /*
	  - Número del cliente.
	  - String de 24 caracteres máximo.
	  - Opcional.
	  */
}
```

**Formato de Respuesta Satisfactoria**: Status 201

```typescript
{
	"id": number
}
```

**Prueba**:

![Add Customer Postman](./pictures/Customer/add_customer.png)

***

## ✔ Update Customer

**Descripción**: Modifica un registro de la tabla Customer con la información especificada en el Body.

**URL**: /api/Customer/{id}

**Método  HTTP**: PUT

**Parámetros del URL**:
* **id**: Id del cliente solicitado. Número entero positivo.

**Parámetros del Body**:

```typescript
{
	"id": number,
  	  /*
      - ID del cliente.
	  - Entero de 5 números máximo.
      - Obligatorio.
      */

	"company": string,
      /*
      - Nombre de la compañía del cliente.
      - String de 40 caracteres máximo.
      - Opcional.
      */

	"contactFullName": string,
      /*
      - Nombre completo del cliente.
      - String de 30 caracteres máximo.
      - Opcional.
      */

	"contactPosition": string,
      /*
      - Puesto del cliente.
      - String de 30 caracteres máximo.
      - Opcional.
      */

	"contactPhone": string
	  /*
	  - Número del cliente.
	  - String de 24 caracteres máximo.
	  - Opcional.
	  */
}
```

**Formato de Respuesta Satisfactoria**: Status 204

**Prueba**:

![Update Customer Postman](./pictures/Customer/update_customer.png)

***

## ✔ Delete Customer

**Descripción**: Elimina el registro del cliente especificado.

**URL**: /api/Customer/{id}

**Método  HTTP**: DELETE

**Parámetros del URL**:
* **id**: Id del cliente solicitado. Número entero positivo.

**Parámetros del Body**: Ninguno

**Formato de Respuesta Satisfactoria**: Status 204

**Prueba**:

![Delete Customer Postman](./pictures/Customer/delete_customer.png)

***

## ✔ Get Page

**Descripción**: Devuelve un JSON con 10 registros de la tabla de clientes. Los registros que se muestren dependerán de la página solicitada.

**URL**: /api/Customer/pages/{requestedPage}

**Método  HTTP**: GET

**Parámetros del URL**:
* **requestedPage**: Número de la página que se desea obtener de la tabla de clientes. Número entero positivo.

**Parámetros del Body**: Ninguno

**Formato de Respuesta Satisfactoria**: Status 200

```typescript
{
	"previousPage":  number | null,
	"currentPage":  number | null,
	"nextPage":  number | null,
	"lastPage":  number,
	"responseList":  [
		{
			"id": number,
			"company": string,
			"contactFullName": string,
			"contactPosition": string,
			"contactPhone": string
		},
		//...
	]
}
```

**Prueba**:

![Get Page Postman](./pictures/Customer/get_page.png)

***

### ✔ Get All Customers

**Descripción**: Devuelve un JSON con todos los registros de la tabla de clientes. Los registros que se muestren dependerán de la página solicitada.

**URL**: /api/Customer/pages/{requestedPage}

**Método  HTTP**: GET

**Parámetros del URL**:
* **requestedPage**: Número de la página que se desea obtener de la tabla de clientes. Número entero positivo.

**Parámetros del Body**: Ninguno

**Formato de Respuesta Satisfactoria**: Status 200

```typescript
{
	[
		{
			"id": number,
			"company": string,
			"contactFullName": string,
			"contactPosition": string,
			"contactPhone": string
		},
		//...
	]
}
```

**Prueba**:

![Get All Customers Postman](./pictures/Customer/get_all_customers.png)