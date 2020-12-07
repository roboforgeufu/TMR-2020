 

// Estrutura básica do algoritmo (baseado em c pq a gente n tava com a documentação no dia)
/*
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
*/

/* Começa aqui */
int ms_meioq = 575;

bc.actuatorSpeed(150);
bc.actuatorUp(1000);

Action <float, int> turn = (forca, lado) => {
	int[] values = {0, 90, 180, 270, 360};
	int i, menorDifIdx = -1;

	float menorDif = 0;
	for(i = 0; i < 5; i++){
		if(i == 0){
			menorDif = Math.Abs(values[i] - bc.compass());
			menorDifIdx = i;
		}
		if(Math.Abs(values[i] - bc.compass()) < menorDif){
			menorDif = Math.Abs(values[i] - bc.compass());
			menorDifIdx = i;
		}
	}


	float g_inicial = values[menorDifIdx];
	bc.printLCD(1, menorDifIdx.ToString());
	bc.printLCD(2, menorDif.ToString());
	bc.printLCD(3, bc.compass().ToString());


	bc.wait(500);
	bc.clearLCD();

	if(lado == 1){
		while(bc.compass() > values[(menorDifIdx-1)%5]){
			bc.onTF((lado)*forca , (-lado)*forca);
			bc.printLCD(2, Math.Abs(bc.compass() - g_inicial).ToString());
		}
	}

	bc.onTF(0, 0);
};


while(true){
	//Ligar os motores pra frente
	bc.onTF(300, 300);
	if(bc.distance(1) > 40){
		// Curva na direção padrão
		
		bc.onTF(300, 300);
		bc.wait(ms_meioq);
		
		bc.onTF(0, 0);
		turn(1000, -1);

		while(bc.distance(1) > 40){
			bc.onTF(300, 300);
		}

	}else if(bc.distance(0) < 35){
		// Frente bloqueada
		if(bc.distance(2) > 35){
			// Curva na direção oposta
			turn(1000, 1);
		} else {
			// Meia volta
			turn(1000, 1);
			turn(1000, 1);
		}
	}
}

bc.onTF(0, 0);