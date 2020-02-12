
La soluci�n consta de 2 partes.
La API se public� en una cuenta free de Microsoft azure, y el desarrollo esta hecho con .Net Core con base de datos MongoDB.


1)- Una aplicaci�n de consola, para correr de manera local, donde se necesita tener instalado y corriendo el servicio de mongodb.
	=> ejecutando directamente la consola, se generan datos en la base para los 10 a�os, y se imprime un reporte para:
		- Los per�odos de sequ�a
		- Los per�odos de lluvia 
		- Los per�odos de condiciones �ptimas
		- Los per�odos "normales", que son los d�as que no cumplen ninguna de las 3 condiciones anteriores
		- El pico m�ximo de lluvia
	=> corriendo bajo el parametro -d se depura la base de datos.

2)- Se publica un API para lo siguiente:
	=> Depurar la base de datos
	https://webapiplanets.azurewebsites.net/weatherforecast/ClearWeatherForecast

	=> Generar datos y obtener un informe similar a como se ejecuta por consola.
	https://webapiplanets.azurewebsites.net/weatherforecast/GetWeatherForecast

	=> Obtener para los pr�ximos 10 a�os, dia a dia, las condiciones (lluvia/sequ�a/ideal/normal)
	https://webapiplanets.azurewebsites.net/weatherforecast/GenerateWeatherForecast

	=> Obtener para un d�a particular (n�mero de d�a, no fecha) las condiciones (lluvia/sequ�a/ideal/normal) 
	https://webapiplanets.azurewebsites.net/WeatherForecast/GetWeatherForecast?day=355



