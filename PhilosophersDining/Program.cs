using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhilosophersDining
{
    class Program
    {
        const int Pensando = 0 , TieneHambre  = 1, Comiendo =  2;
        static int[] Estado = new int[5];

        static Thread TFilosofo1, TFilosofo2, TFilosofo3, TFilosofo4, TFilosofo5;        
        static Semaphore Mutex;
        static Semaphore[] Filosofos;

        static void Main(string[] args)
        {
            //Se inicializa los Semaforos por Filosofo
            Filosofos = new Semaphore[5];
            Filosofos[0] = new Semaphore(1, 1);
            Filosofos[1] = new Semaphore(1, 1);
            Filosofos[2] = new Semaphore(1, 1);
            Filosofos[3] = new Semaphore(1, 1);
            Filosofos[4] = new Semaphore(1, 1);

            Mutex = new Semaphore(1, 1);
            //Se iniciliza los Threads por Filosofo
            TFilosofo1 = new Thread(Filosofo1);
            TFilosofo2 = new Thread(Filosofo2);
            TFilosofo3 = new Thread(Filosofo3);
            TFilosofo4 = new Thread(Filosofo4);
            TFilosofo5 = new Thread(Filosofo5);

            TFilosofo1.Start();
            TFilosofo2.Start();
            TFilosofo3.Start();
            TFilosofo4.Start();
            TFilosofo5.Start();

        }


        //Threads, 1 * Filosofo
        static private void Filosofo1()
        {
            while (true)
            {
                Pensar(0);
                TomarTenedores(0);
                Comer(0);
                DejarTenedores(0);
            }
        }
        static private void Filosofo2()
        {
            while (true)
            {
                Pensar(1);
                TomarTenedores(1);
                Comer(1);
                DejarTenedores(1);
            }
        }
        static private void Filosofo3()
        {
            while (true)
            {
                Pensar(2);
                TomarTenedores(2);
                Comer(2);
                DejarTenedores(2);
            }
        }
        static private void Filosofo4()
        {
            while (true)
            {
                Pensar(3);
                TomarTenedores(3);
                Comer(3);
                DejarTenedores(3);
            }
        }
        static private void Filosofo5()
        {
            while (true)
            {
                Pensar(4);
                TomarTenedores(4);
                Comer(4);
                DejarTenedores(4);
            }
        }
        //Metodos Auxiliares
        private static void Pensar(int Filosofo)
        {
            Mutex.WaitOne();
            Random r = new Random();
            Thread.Sleep(r.Next(100, 1000));
            Estado[Filosofo] = Pensando;
            Console.WriteLine("Filosofo {0} esta pensando", Filosofo);
            Mutex.Release();
            Filosofos[Filosofo].WaitOne();
        }
        private static void TomarTenedores(int Filosofo)
        {
            Mutex.WaitOne();    
            Estado[Filosofo] = TieneHambre;
            IntentarTomarTenedores(Filosofo);
            Mutex.Release();
        }

        private static void IntentarTomarTenedores(int i)
        {
            if (Estado[i] == TieneHambre &&
                Estado[(i + 4) % 5] != Comiendo &&
                Estado[(i + 1) % 5] != Comiendo)
            {
                Estado[i] = Comiendo;
                Filosofos[i].Release();
                Console.WriteLine("El Filosofo {0} tomo los tenedores y esta comiendo", i);
            }
        }
        private static void Comer(int Filosofo)
        {
            Mutex.WaitOne();      
            //Do something, Eat
            Mutex.Release();
        }
        private static void DejarTenedores(int Filosofo) 
        {
            Mutex.WaitOne();
            //if(Estado[Filosofo] != Pensando
            Estado[Filosofo] = Pensando;
            Console.WriteLine("Filosofo {0} dejo los tenedores", Filosofo);
            IntentarTomarTenedores((Filosofo + 4) % 5);
            IntentarTomarTenedores((Filosofo + 1) % 5);           
            Mutex.Release();


        }
    }
}
