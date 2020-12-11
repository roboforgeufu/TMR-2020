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

double direcaoInicial = 0;
double direcaoFinal = 0;

Action curvaFixadaDireita = () => {
	//funcao que faz a curva de 90 graus
	direcaoInicial = Math.Round(bc.compass()/90)*90;
    direcaoFinal = (90* ( Math.Round(bc.compass()/90) +1 )) % 360;
	
	if(direcaoFinal == 0){
        //Adapta o resultado do resto pro caso em que 
        // a curva vai crescendo até chegar em 0 = 360
        direcaoFinal = 359;
    }

    if(direcaoInicial == 360){
        bc.onTF(-1000, 1000);
        bc.wait(500);
    }

    
    while( bc.compass() < direcaoFinal & bc.compass() < 360){
        bc.onTF(-1000, 1000);
    }
    bc.onTF(0, 0);

};

Action curvaFixadaEsquerda = () => {
	//funcao que faz a curva de 90 graus
	direcaoInicial = Math.Round(bc.compass()/90)*90;
    direcaoFinal = (90* ( Math.Round(bc.compass()/90) -1 )) % 360;
	
	bc.printLCD(1, direcaoInicial.ToString());
	bc.printLCD(2, direcaoFinal.ToString());

	if(direcaoFinal == -90){
        //Adapta o resultado do resto pro caso em que 
        //a curva vai crescendo até chegar em 0 = 360
        direcaoFinal = 270;
    }

    if(direcaoInicial == 0){
        bc.onTF(1000, -1000);
        bc.wait(500);
    }
    
    while( bc.compass() > direcaoFinal & bc.compass() > 1){
        bc.onTF(1000, -1000);
    }
    bc.onTF(0, 0);
	bc.printLCD(3, bc.compass().ToString());
};

Func < int> random = () => { //funcao que retornchegar em aleatorio sendo 1 ou -1
	int a = bc.randomLimits(1, 2);
	if (a == 1) {
		return (1);
	} else {
		return (-1);
	}
};


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
			curvaFixadaDireita(1);
			}
		else if (bc.distance(0) <= distancia_sensor) {
			if(bc.distance(2) >= distancia_sensor){
				bc.printLCD(1, "CURVA A ESQUERDA");
				curvaFixadaEsquerda(1);
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
			curvaFixadaEsquerda(1);
			}
		else if (bc.distance(0) <= distancia_sensor) {
			if(bc.distance(1) >= distancia_sensor){
				bc.printLCD(1, "CURVA A DIREITA");
				curvaFixadaDireita(1);
			} 
			else{
				bc.printLCD(1, "MEIA VOLTA");
				bc.onTFRot(vel_ang, direcao*180);
			}
		}
		
	}
}	

bc.onTF(0, 0);