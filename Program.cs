using System.Diagnostics;
public class EightQueens
{


    public static int[,] Randomizer(){
        
        int [,] tahta = {{0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0}}; 
        
        

        Random random = new();
            
        

        for (int i=0 ;i<8;i++){

            int randomSayi = random.Next(0, 8);
            tahta[randomSayi,i] = 1;

        }
        return tahta;

}

    public static int CostCalculator(int [,] tahta, int [,] locationlist){

        int cost=0;



        for (int i=0; i<8 ; i++){


            for (int j=0 ; j<8 ; j++){

                if ( locationlist[j,1]==locationlist[i,1]){
                    cost+=1;

                };
                 //kendini bulduğu durum


            }
            cost-=1;
            int a = locationlist[i,0];
            int b = locationlist[i,1];

            while(a<8 && a>=0 && b<8 && b>=0){

                a+=1;
                b+=1;

                for (int q = 0 ; q<8 ; q++){
                        
                    if(locationlist[q,0]==a && locationlist[q,1]==b){
                        cost+=1;
                    }
                }
   
            }
            a = locationlist[i,0];
            b = locationlist[i,1];
            while(a<8 && a>=0 && b<8 && b>=0){

                a-=1;
                b+=1;

                for (int q = 0 ; q<8 ; q++){
                        
                    if(locationlist[q,0]==a && locationlist[q,1]==b){
                        cost+=1;
                    }
                }
   
            }
            a = locationlist[i,0];
            b = locationlist[i,1];
            while(a<8 && a>=0 && b<8 && b>=0){

                a+=1;
                b-=1;

                for (int q = 0 ; q<8 ; q++){
                        
                    if(locationlist[q,0]==a && locationlist[q,1]==b){
                        cost+=1;
                    }
                }
   
            }
            a = locationlist[i,0];
            b = locationlist[i,1];
            while(a<8 && a>=0 && b<8 && b>=0){

                a-=1;
                b-=1;

                for (int q = 0 ; q<8 ; q++){
                        
                    if(locationlist[q,0]==a && locationlist[q,1]==b){
                        cost+=1;
                    }
                }
   
            }

        }



        return cost;


    }

    public static int [,] LocationList( int [,] tahta){

        int [,] locationlist = {{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0},{0,0}};



        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (tahta[j,i]==1){
                    locationlist[i,0]=i;
                    locationlist[i,1]=j;

                }
            }
            
        }
        return locationlist;

    }
    public static (int [,],int[,],int) MoveTahta(int [,] tahta,int [,] locationlist, int cost){
        
        int gecicicost=0;
        int bestCost = cost;
        int[,] kopyaMatris1 = (int[,])tahta.Clone();
        int[,] kopyaList= (int[,])locationlist.Clone();

        
        for (int i = 0; i<8 ; i++){
            List<int> sayilar = [0, 1, 2, 3, 4, 5 ,6 ,7];
            int a = locationlist[i,0];
            int b = locationlist[i,1];
            sayilar.Remove(a);
            int[,] kopyaLocationList = (int[,])locationlist.Clone();

            
            for (int j = 0; j<7 ; j++){
                int[,] kopyaMatris = (int[,])tahta.Clone();

                kopyaMatris[b,a]=0;
                kopyaMatris[sayilar[j],a]=1;

                kopyaLocationList= LocationList(kopyaMatris);
                
                
                gecicicost= CostCalculator(kopyaMatris,kopyaLocationList);
                if (bestCost>gecicicost){
                    bestCost=gecicicost;
                    kopyaMatris1=kopyaMatris;
                    kopyaList=kopyaLocationList;

                }

        }

        }
   


        return (kopyaMatris1,kopyaList,bestCost);
    }

    public static void Print(int [,] tahta){

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Console.Write(tahta[i, j] + "\t" );
            }
            Console.WriteLine(); // Bir sonraki satıra geç
        }
    }

}

class Main1{

    static void Main(string[] args){
        Stopwatch stopwatch = new();

        EightQueens deneme= new();
        int[,] tahta = EightQueens.Randomizer();

        int[,] locationlist = EightQueens.LocationList(tahta);
        int cost = EightQueens.CostCalculator(tahta,locationlist);
        int cost1 = 0;   
        int count = 0;
        int count1=0;
        int restartCounter=0;
        int moveCounter = 0;
        List<int> resCounter = [];
        List<int> movCounter = [];
        List<double> secCounter = [];
        

        while(count<15){
            count1++;
            stopwatch.Start();

            if (count1==1){
                Console.WriteLine($"Deneme: {count+1} 1.Durum: Restart sayisi:{restartCounter}");
                EightQueens.Print(tahta);
                Console.WriteLine("--------------------------------------------------");

            }
            
            
            
            cost1=cost;
            (tahta,locationlist,cost)= EightQueens.MoveTahta(tahta,locationlist,cost);
            moveCounter+=1;

            Console.WriteLine($"Deneme: {count+1} {count1+1}.Durum: Restart sayisi: {restartCounter}");
            EightQueens.Print(tahta);

            Console.WriteLine("--------------------------------------------------");

            
            if (cost1==cost){
                
                tahta = EightQueens.Randomizer();
                locationlist = EightQueens.LocationList(tahta);
                cost = EightQueens.CostCalculator(tahta,locationlist);
                restartCounter+=1;
                count1=0;
                Console.WriteLine("restart atildi");

                Console.WriteLine("--------------------------------------------------");

                
            }
            else if(cost==0){
                Console.WriteLine("Basarili");
                count+=1;
                movCounter.Add(moveCounter);
                moveCounter=0;
                
                resCounter.Add(restartCounter);
                restartCounter=0;
                stopwatch.Stop();
                double elapsed = stopwatch.Elapsed.TotalSeconds;
                secCounter.Add(elapsed);
                stopwatch.Reset();
                count1=0;
                
            }


        }
        double totalRes=0;
        double totalMov=0;
        double totalSec=0;

        Console.WriteLine("                        restart sayısı    |    yer değiştirme sayısı    |    süre");
        for (int i = 0; i < 15; i++)
        {
            string restartCount = resCounter[i].ToString().PadLeft(20);
            string moveCount = movCounter[i].ToString().PadLeft(25);
            string time = secCounter[i].ToString().PadLeft(25);
            Console.WriteLine($"{i + 1}. Deneme: |{restartCount}{moveCount}{time}");
            totalRes+=resCounter[i];
            totalMov+=movCounter[i];
            totalSec+=secCounter[i];
        }
        Console.WriteLine();

        Console.WriteLine($"Ortalama:          {totalRes/15}                {totalMov/15}          {totalSec/15}");

    }

}

