
La solución consta de 2 partes.
La API se publicó en una cuenta free de Microsoft azure, y el desarrollo esta hecho con .Net Core con base de datos MongoDB.


1)- Una aplicación de consola, para correr de manera local, donde se necesita tener instalado y corriendo el servicio de mongodb.
	=> ejecutando directamente la consola, se generan datos en la base para los 10 años, y se imprime un reporte para:
		- Los períodos de sequía
		- Los períodos de lluvia 
		- Los períodos de condiciones óptimas
		- Los períodos "normales", que son los días que no cumplen ninguna de las 3 condiciones anteriores
		- El pico máximo de lluvia
	=> corriendo bajo el parametro -d se depura la base de datos.

2)- Se publica un API para lo siguiente:
	=> Depurar la base de datos
	https://webapiplanets.azurewebsites.net/weatherforecast/ClearWeatherForecast

	=> Generar datos y obtener un informe similar a como se ejecuta por consola.
	https://webapiplanets.azurewebsites.net/weatherforecast/GetWeatherForecast

	=> Obtener para los próximos 10 años, dia a dia, las condiciones (lluvia/sequía/ideal/normal)
	https://webapiplanets.azurewebsites.net/weatherforecast/GenerateWeatherForecast

	=> Obtener para un día particular (número de día, no fecha) las condiciones (lluvia/sequía/ideal/normal) 
	https://webapiplanets.azurewebsites.net/WeatherForecast/GetWeatherForecast?day=355



