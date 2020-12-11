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

Action < int> andar = (quadrados) => {

for (int i=0; i<quadrados;i++){
	bc.onTF(300, 300);
	bc.wait(1250);
	bc.onTF(0, 0);
	}
};

Func < int> random = () => {
	int a = bc.randomLimits(1, 2);
	if (a == 1) {
		return (1);
	} else {
		return (-1);
	}
};

int direcao = 1;



while(true){
	//Ligar os motores pra frente
	andar(1);
	bc.wait(500);
	if(bc.distance(1) >= 40){
		// Curva na direção padrão
		bc.onTFRot(400, direcao*90);}
	else if(bc.distance(0) < 35){
		// Frente bloqueada
		if(bc.distance(2) > 35){
			// Curva na direção oposta
			bc.onTFRot(400, direcao*-90);
			andar(1);
		} else {
			// Meia volta
			bc.onTFRot(400, direcao*180);
		}
	}
}
		

bc.onTF(0, 0);