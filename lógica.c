int main() {
	//Sensor 1 > Frente
	//Sensor 2 > Direita
	//Sensor 3 > Esquerda 

	// Calcular as direçoes de forma aleatoria  
	while(1){
		//Ligar os motores pra frente
		if(sensorLadoPrincipal.valor > 35){
			// Curva na direção padrão
		} else if(sensorFrente.valor < 60){
			// Frente bloqueada
			if(sensorLadoOposto.valor > 35){
				// Curva na direção oposta
			} else {
				// Meia volta
			}
		}
	}

	return 0;
}
