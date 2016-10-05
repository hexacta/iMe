# iMeDev
Repositorio de investigacion/experimentos

## Git for Windows
Al instalar el cliente git para windows, seleccionar la opcion que integra en el command prompt de windows. Esto es porque npm (el gesto de paquetes de node), muchas veces se conecta a github para obtener los paquetes a instalar. Si git no esta correctamente configurado, muchos paquetes no se instalaran correctamente.

##Requisitos:
1. Instalar node.js
2. instalar bower
 1. `npm install -g bower`
3. ejecutar los siguiente comandos (primero cambiarse a la carpeta del cliente web)
  1. `npm install`
  2. `bower install`
4. Instalar gulp y gulp cli
  1. `npm install -g gulp`
  2. `npm install -g gulp-cli`
5. Instalar generador 
  1. `npm i -g yo`
  2. `npm i -g generator-fountain-webapp`


## Como levantar el cliente web

Ejecutar `gulp build` y luego `gulp serve`

## Reglas de desarrollo
* Cualquiera puede meter mano
* No se puede hacer commit al branch master, por cada feature o agregado, crear un branch y solicitar un pull request.


### Librerias utilizadas
####Twitter

https://github.com/JoeMayo/LinqToTwitter/wiki