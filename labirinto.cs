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
//funcao que anda de um centro do quadrado para o proximo
for (int i=0; i<quadrados;i++){
	bc.onTF(300, 300);
	bc.wait(1325);
	bc.onTF(0, 0);
	}
};

Func < int> random = () => { //funcao que retorna valor aleatorio sendo 1 ou -1
	int a = bc.randomLimits(1, 2);
	if (a == 1) {
		return (1);
	} else {
		return (-1);
	}
};

int direcao = random();
float distancia_sensor = 60;
int vel_ang = 400;
int ajustar = 0;

//Direita
if (direcao == 1){
	while(true){
		if(ajustar == 0){
		bc.onTF(-300, -300);
		bc.wait(100);
		bc.onTF(0, 0);
		ajustar = 1;
		}
		bc.printLCD(2, direcao.ToString());
		//Ligar os motores pra frente
		bc.printLCD(1, "SEGUIR RETO");
		andar(1);
		bc.wait(500);
		if(bc.distance(1) >= distancia_sensor){
			// Curva na direção padrão
			bc.printLCD(1, "CURVA A DIREITA");
			bc.onTFRot(vel_ang, direcao*89);}
		else if (bc.distance(0) <= distancia_sensor) {
			if(bc.distance(2) >= distancia_sensor){
				bc.printLCD(1, "CURVA A ESQUERDA");
				bc.onTFRot(vel_ang, direcao*-89);
			} 
			else{
				bc.printLCD(1, "MEIA VOLTA");
				bc.onTFRot(vel_ang, direcao*180);
			}
		}
		
	}
}
else{ //Esquerda
	while(true){
			if(ajustar == 0){
		bc.onTF(-300, -300);
		bc.wait(100);
		bc.onTF(0, 0);
		ajustar = 1;
		}
		bc.printLCD(2, direcao.ToString());
		//Ligar os motores pra frente
		bc.printLCD(1, "SEGUIR RETO");
		andar(1);
		bc.wait(500);
		if(bc.distance(2) >= distancia_sensor){
			// Curva na direção padrão
			bc.printLCD(1, "CURVA A ESQUERDA");
			bc.onTFRot(vel_ang, direcao*88);}
		else if (bc.distance(0) <= distancia_sensor) {
			if(bc.distance(1) >= distancia_sensor){
				bc.printLCD(1, "CURVA A DIREITA");
				bc.onTFRot(vel_ang, direcao*-88);
			} 
			else{
				bc.printLCD(1, "MEIA VOLTA");
				bc.onTFRot(vel_ang, direcao*180);
			}
		}
		
	}
}	

bc.onTF(0, 0);