# Product 🛒
El Controlador Product permite a un usuario autenticado interactuar con la table Product de la Base de Datos Northwind.

## ✔ Get One Product

**Descripción**: Devuelve un JSON con la información del producto solicitado.

**URL**: /api/Product/{id}

**Método  HTTP**: GET

**Parámetros del URL**:
* **id**: Id del producto solicitado. Número entero positivo.

**Parámetros del Body**: Ninguno

**Formato de Respuesta Satisfactoria**: Status 200

```typescript
{
	"id": number,
	"name": string,
	"isDiscontinued": boolean,
	"price": number | null
}
```

**Prueba**:

![Get One Product Postman](./pictures/Product/get_one_product.png)

***

## ✔ Add Product

**Descripción**: Agrega un registro de la tabla Product con la información especificada en el Body. Devuelve un JSON con el ID registrado para el product.

**URL**: /api/Product

**Método  HTTP**: POST

**Parámetros del URL**: Ninguno

**Parámetros del Body**:

```typescript
{
	"name": string,
      /*
      - Nombre del producto.
      - 40 caracteres máximo.
      - Obligatorio.
      */

	"isDiscontinued": boolean,
      /*
      - Estatus del producto.
      - Obligatorio.
      */

	"price": number
      /*
      - Precio del producto.
      - Decimal positivo.
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

![Add Product Postman](./pictures/Product/add_product.png)

***

## ✔ Update Product

**Descripción**: Modifica un registro de la tabla Product con la información especificada en el Body.

**URL**: /api/Product/{id}

**Método  HTTP**: PUT

**Parámetros del URL**:
* **id**: Id del producto solicitado. Número entero positivo.

**Parámetros del Body**:

```typescript
{
	"name": string,
      /*
      - Nombre del producto.
      - 40 caracteres máximo.
      - Obligatorio.
      */

	"isDiscontinued": boolean,
      /*
      - Estatus del producto.
      - Obligatorio.
      */

	"price": number
      /*
      - Precio del producto.
      - Decimal positivo.
      - Opcional.
      */
}
```

**Formato de Respuesta Satisfactoria**: Status 204

**Prueba**:

![Update Product Postman](./pictures/Product/update_product.png)

***

## ✔ Delete Product

**Descripción**: Elimina el registro del producto especificado.

**URL**: /api/Product/{id}

**Método  HTTP**: DELETE

**Parámetros del URL**:
* **id**: Id del product solicitado. Número entero positivo.

**Parámetros del Body**: Ninguno

**Formato de Respuesta Satisfactoria**: Status 204

**Prueba**:

![Delete Product Postman](./pictures/Product/delete_product.png)

***

## ✔ Get Page

**Descripción**: Devuelve un JSON con 10 registros de la tabla de productos. Los registros que se muestren dependerán de la página solicitada.

**URL**: /api/Product/pages/{requestedPage}

**Método  HTTP**: GET

**Parámetros del URL**:
* **requestedPage**: Número de la página que se desea obtener de la tabla de productos. Número entero positivo.

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
			"name": string,
			"isDiscontinued": boolean,
			"price": number | null
		},
		//...
	]
}
```

**Prueba**:

![Get Page Postman](./pictures/Product/get_page.png)

***

### ✔ Get All Products

**Descripción**: Devuelve un JSON con todos los registros de la tabla de productos. Los registros que se muestren dependerán de la página solicitada.

**URL**: /api/Product/pages/{requestedPage}

**Método  HTTP**: GET

**Parámetros del URL**:
* **requestedPage**: Número de la página que se desea obtener de la tabla de productos. Número entero positivo.

**Parámetros del Body**: Ninguno

**Formato de Respuesta Satisfactoria**: Status 200

```typescript
{
	[
		{
			"id": number,
			"name": string,
			"isDiscontinued": boolean,
			"price": number | null
		},
		//...
	]
}
```

**Prueba**:

![Get All Products Postman](./pictures/Product/get_all_products.png)


# [Volver](../README.md) ⏪