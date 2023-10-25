# Polizas
# Se debe instalar el sdk de .Net 6.0
# Instalar mongoDB, crear una base de datos llamada local dentro de esta una colección llamada Polizas
# Ejecutar desde un terminal el comando dotnet run para el arranque del servidor del proyecto
# Los endpoind son:
# Login para obtener jwt token http://localhost:5000/api/Auth/Login metodo post, pasar en el cuerpo en json {
#    "Username": "usuarioDemo",
#    "Password": "passwordDemo"
# }
# Crear Poliza Post http://localhost:5000/api/Polizas pasar el cuerpo en json ej: {
#    "NumeroPoliza": 1234567,
#    "NombreCliente": "Test Client",
#    "IdentificacionCliente": "1111111",
#    "FechaNacimientoCliente": "1982-01-01",
#    "FechaEmision": "2023-10-24",
#    "FechaInicioVigencia": "2023-10-24",
#    "FechaFinVigencia": "2024-10-24",
#    "Coberturas": ["Cobertura1", "Cobertura2"],
#    "ValorMaximoCubierto": 60000.00,
#    "CiudadResidencia": "Medellin",
#    "DireccionResidencia": "Cl 2",
#    "PlacaAutomotor": "AVG-13233",
#    "ModeloAutomotor": "2022",
#    "TieneInspeccion": true
# }
# y pasar token de autorización en cabecera Authorization Bearer {Token}
# Listar por placa Get http://localhost:5000/api/Polizas/BuscarPorPlaca/AVG-13233 igualmente pasar token
# Listar por poliza Get http://localhost:5000/api/Polizas/BuscarPorNumeroPoliza/1234567 igualmente pasar token