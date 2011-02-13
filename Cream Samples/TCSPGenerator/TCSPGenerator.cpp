/* 

   Some parts of this program is taken from http://www.lirmm.fr/~bessiere/generator.html
   urbcsp.c-- generates uniform random binary constraint satisfaction problems

   PROGRAM: CCTCSP randomly generator (gcctcsp.cpp) based on MODEL RB.  
   BY     : Amrudee Sukpan, Computer Science, University of Regina, SK, Canada.
   Compile: On antar.cs.uregina.ca or alba.cs.uregina.ca
   Command: CC gcctcsp.cpp -o gcctcsp
   Purpose: To randomly generate CCTCSP instance
   Input  : #variables, alpha, r, tightness, #composite-variables, domain-composite-variable, initial-variable, activity-constraints, horizon, seed, #instance
   Output : a CCCSP instance // can be saved in a text file then later use it to the CCCSP-Solver

*/

//Header files
#include <stdafx.h>
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <bitset>
#include <math.h>
#include <vector>

#define COMPOSITE_BASE 500           // define base internal ID for composite variables.
#define COMPOSITE_CONSTRAINTS 0.45   // define density of single variables beloning to composite variables
#define ALLEN_RELATIONS 13           // thridthteen basic Allen's Relations
#define MIN_DURATION 10              // Minimum allowance duration
#define STEP 5                       // Maximum allowance step
#define TIGHTNESS_RANGE 0.15


//===========================================
//Define integer for each Allen relation

#define P_R 0
#define p_R 1
#define S_R 2
#define s_R 3
#define M_R 4
#define m_R 5
#define O_R 6
#define o_R 7
#define D_R 8
#define d_R 9
#define F_R 10
#define f_R 11
#define E_R 12


using namespace std;

//----------function pototype declaration-------------------------------
float ran2(long *idum);
void StartCSP(int N, int K, int instance);
void EndCSP();
void AddConstraint(int var1, int var2);
void AddNogood(int val1, int val2);
void PrintRelation(int R);
int Relation(struct INTERVAL X, struct INTERVAL Y);
bool Satisfied(struct INTERVAL X,int R,struct INTERVAL Y);
struct INTERVAL AssociateInterval(int Value,struct SOPO Sopo);
int MakeCCTSP(int N, int D, int C, int T, int CN, int CD, long *Seed,int PP,int H,bool Sat);
void Num_Sym(bitset<ALLEN_RELATIONS> **C, int NN,struct SOPO *SOPOs);
bitset<ALLEN_RELATIONS> Inverse(bitset<ALLEN_RELATIONS> Rel);
void ConstraintTightness(struct SOPO *SOPOs,int NN,int DZ);
void CountConstraint(struct SOPO *SOPOs,int NN,int DZ,int i,int j, int *Tightness);
bool SetRelations(int T,int Dz,bitset<ALLEN_RELATIONS> &RMatrix, struct SOPO XSOPO,struct SOPO YSOPO,int x,int y, bool Sat, int *Sol);
int f(int nn,int yy, int Tightness[ALLEN_RELATIONS],bitset<ALLEN_RELATIONS> &RMatrix);
int max(int X,int Y);
//--------------------------------------------------------------------


struct ACT
{ 
  int X;
  int A;
  int Y;
};

struct SOPO
{ int Begin;
  int End;
  int Duration;
  int Step;
};

struct INTERVAL
{ 
  int start;
  int end;
};

//=================== MAIN PROGRAM ==================================

//------Globle variables-------
float INV;
int ACTIVITY_PROB; 
float DISTRIBUTE_DOMAIN; 
int Tightness_Allen;
int MAX_DURATION;
//-----------------------------

int main(int argc, char* argv[])
{
  bool **CMatrix;
  bitset<ALLEN_RELATIONS> **RMatrix;
  int i,j,r,t;
  int PossibleCTs, PossibleNGs;       /* CT means "constraint" */
  long selectedCT, selectedNG;
  int var1, var2, val1, val2;
  static int instance;
  int N, D, C, T, I, CN, CD ,H,NN;
  long S;
  bool Sat=false;

  //check number of input parameters
  if (argc < 10)
    {
      //printf("usage: gcctcsp #vars #alpha #r #p #composites #domains #iv #act #horizon seed #instances [-sat]\n");
      cout << "=======================================================================================" << endl;
      cout << "usage:: gcctcsp #Var Alpha(>0.5) #r #p #composite #c-domain #iv #act #horizon seed #instances [-sat]\n\n";
      cout << "To save the generated instance into a file, uses the direct command \">\" \n";
      cout << "usage:: gcccsp #Vars Alpha(>0.5) #r #p #composites #domains #iv #act #horizon seed #instances [-sat] > FILENAME \n\n";
      cout << "----------------------------------------------------------------------------------------\n";
      cout << "Var        = the number of simple variables" << endl;    
      cout << "Alpha      = a real number and greater than 0.5 (see Model RB for the full details)" << endl;
      cout << "r          = a real number and greater than 0 (see Model RB for the full details)" << endl;
      cout << "p          = the precent of incompatible pairs/each constraint (tightness)"<<endl;
      cout << "Composite  = the number of composite variables" << endl;
      cout << "c-domain   = the domain size of composite variables" << endl;
      cout << "iv         = the precent of initial variables (default active variables)" << endl;
      cout << "act        = the precent of activity constraints" << endl;
      cout << "horizon    = the maximum time time for the problem" << endl;
      cout << "seed       = randomly generated seed" << endl;
      cout << "instance   = the number of instances that want to randomly generate by these given parameters"<< endl;
      cout << "-sat       = an optional parameter to force the generator to generat consistent instances" << endl;
      cout << "---------------------------------------------------------------------------------------"<<endl;
      cout << "ex  % gcctcsp 20 0.6 0.5 0.5 5 3 0.7 0.01 300 999 1 " << endl;
      cout << "ex  % gcctcsp 20 0.6 0.5 0.5 5 3 0.7 0.01 300 999 1 > instance1" << endl;
      cout << "======================================================================================="<<endl;
     
      return 0;

    }
  //set input parameters

  N = atoi(argv[1]);
  D = (int) pow((double) N,atof(argv[2]));
  C = (int) (atof(argv[3])* N* log((double)N));
  T=(int) (atof(argv[4])*pow((double) D, 2));
  Tightness_Allen=(int) (atof(argv[4])*ALLEN_RELATIONS); // use to escape local optimal when generate relations
  CN= atoi(argv[5]);
  CD= atoi(argv[6]);
  INV= atof(argv[7]);
  ACTIVITY_PROB=(int) ((D*N+CN*CD)*atof(argv[8]));
  H = atoi(argv[9]);
  MAX_DURATION=H/4;
  S = atoi(argv[10]);  
  I = atoi(argv[11]);
  NN=N+CN*CD;

  // SET DISTRIBUTED_DOMAIN
  if ((atof(argv[4]) <= 0.2) && (atof(argv[4])>=0.1))
    DISTRIBUTE_DOMAIN= 0.2;
  else if((atof(argv[4]) <= 0.3) && (atof(argv[4])>=0.21))
    DISTRIBUTE_DOMAIN= 0.3;
  else if((atof(argv[4]) <= 0.4) && (atof(argv[4])>=0.31))
    DISTRIBUTE_DOMAIN= 0.4;
  else if((atof(argv[4]) <= 0.5) && (atof(argv[4])>=0.41))
    DISTRIBUTE_DOMAIN= 0.7;
  else if((atof(argv[4]) <= 0.6) && (atof(argv[4])>=0.51))
    DISTRIBUTE_DOMAIN= 0.8;
  else if((atof(argv[4]) <= 0.7) && (atof(argv[4])>=0.61))
    DISTRIBUTE_DOMAIN= 0.8;
  else if((atof(argv[4]) <= 0.8) && (atof(argv[4])>=0.71))
    DISTRIBUTE_DOMAIN= 0.9;
  else if((atof(argv[4]) <= 0.9) && (atof(argv[4])>=0.81))
    DISTRIBUTE_DOMAIN= 0.9;
  else if((atof(argv[4]) > 0.9))
    DISTRIBUTE_DOMAIN= 0.9;

  //----------------------------------------------------------
  if ((argc==13) && (!strcmp(argv[12],"-sat"))) Sat=true;
  printf ("Ramdom generated CCTCSP instance parameters : \n");
  printf (" %d  %.2f  %.2f  %.2f %d   %d   %.2f %.2f %d   %d   %d\n", 
	  N,atof(argv[2]),atof(argv[3]),atof(argv[4]),atoi(argv[5]),atoi(argv[6]),atof(argv[7]),atof(argv[8]),H,S,I);
  printf ("#Vars  #Domains #Constraints #Tightness #C-Var #C-Domains #Horizon #Seed #Instances\n");	
  printf (" %d      %d         %d           %d       %d       %d        %d      %d      %d\n",
	  N,D,C,T,CN,CD,H,S,I);
  
  
  double aaa=(float) C/(N*(N-1)/2);
  int PP=(int)(ceil((N+CN)*(N+CN-1)/2 *aaa ))-C;
  printf ("Constraints of composite variables : %d \n",PP/CN*CD*CN);

  /* Seed passed to ran2() must initially be negative. */
  if (S > 0)
    S = -S;

  for (i=0; i<I; ++i)
    if (!MakeCCTSP(N, D, C, T,	CN, CD, &S,PP,H,Sat))
      return 0;
  return 1;
}


int MakeCCTSP(int N, int D, int C, int T, int CN, int CD, long *Seed,int PP,int H,bool Sat)
{
  int PossibleCTs, PossibleNGs;       /* CT means "constraint" */
  unsigned long *CTarray;   /* NG means "nogood pair" */
  long selectedCT, selectedNG;
  int i,j, c, r, t,k,l,civ,nciv,Relations[ALLEN_RELATIONS],selected;
  int IV,*iv,*ivv,NN,step,*Sol,PPP;
  int var1, var2, val1, val2;
  static int instance;
  
  //---------------------------------------------
  /* Check for valid values of N, D, C, and T. */
  if (N < 2)
    {
      printf("MakeURBCSP: ***Illegal value for N: %d\n", N);
      return 0;
    }
  if (D < 2)
    {
      printf("MakeURBCSP: ***Illegal value for D: %d\n", D);
      return 0;
    }
  if (C < 0 || C > (N+CN*CD) * ((N+CN*CD) - 1) / 2)
    {
      printf("MakeURBCSP: ***Illegal value for C: %d\n", C);
      return 0;
    }
  if (T > D*D)
    {
      printf("MakeURBCSP: ***Illegal value for T(Average Allen Relations/edge): %d\n", T);
      return 0;
    }
  if (H < 0 )
    {
      printf("MakeURBCSP: ***Illegal value for H(Positive Value): %d\n", T);
      return 0;
    }

  if (*Seed < 0)      /* starting a new sequence of random numbers */
    instance = 0;
  else
    ++instance;       /* increment static variable */
  //------------------------------------------------------------
  StartCSP(N, D, instance);
  NN=N+CN*CD;
  struct SOPO *VN;
  VN=new struct SOPO[NN];
  bitset<ALLEN_RELATIONS> **RMatrix;
  bool **CMatrix;
  CMatrix =(bool**) malloc(sizeof(bool*)*NN);
  for(i=0;i<NN;i++)
    { 
      CMatrix[i]=(bool*) malloc(sizeof(bool)*NN);
      for(int j=0;j<NN;j++) 
	{ 
	  CMatrix[i][j]=false;
	}
      }

  RMatrix=(bitset<ALLEN_RELATIONS>**) malloc(sizeof(bitset<ALLEN_RELATIONS>*)*NN);
  for(i=0;i<NN;i++)
    { 
      RMatrix[i]=(bitset<ALLEN_RELATIONS>*) malloc(sizeof(bitset<ALLEN_RELATIONS>)*NN);
      for(int j=0;j<NN;j++) 
	{ 
          RMatrix[i][j].set(); // 13 relations are on
	}
      }  
  /*----------------------------------------------------*/
  /* set composite variables and initial variables      */
  
  PossibleCTs = N * (N - 1) / 2;
  //cout << "PossibleCTs= "<<PossibleCTs<<endl;
  CTarray = (unsigned long*) malloc(PossibleCTs * sizeof(unsigned long));
  PossibleNGs=ALLEN_RELATIONS;
  //NGarray = (unsigned long*) malloc(PossibleNGs * sizeof(unsigned long));
  /*----------------------------------------------------*/
  /* set activity constraint                            */
  struct ACT *aa;
  struct ACT a;
  int dd,m,p,ii,ss;
  //j=IV;
  ss=((N+(CN*CD)+CN)*D*(N+(CN*CD)+CN-1));
  aa = (struct ACT*) malloc(sizeof(struct ACT)*ss);
  /*----------------------------------------------------*/

  for(i=0;i<CN;i++)
  { printf("%6d : ", COMPOSITE_BASE+i);
    for(j=0;j<CD;j++)
      { 
	cout << N+j+i*CD << " ";
      }
    cout << endl;
  }
  /*-----------------------------------------------------*/
  /* random initial variables */

   IV=(int) (INV *(N+CN));
  iv = new int[N+CN];
  for (i=0;i<N;i++) iv[i]=i;

  for (i=N;i<N+CN;i++) iv[i]=COMPOSITE_BASE+i-N;

  for (i=0;i<IV;++i)
  {
      r = i + (int) ((N+CN-i) * ran2(Seed));
      c = iv[r];
      iv[r]=iv[i];
      iv[i]=c;				
  }
  
  printf("\nInitial Variables : %d\n",IV);
  nciv=0;
  for (i=0;i<IV;i++) 
    { printf ("   %d  ", iv[i]);
      if (iv[i]>=COMPOSITE_BASE) nciv++;
    }
  cout << endl;

  printf("\nNonactive Variables : %d\n",N+CN-IV);
  for (i=IV;i<N+CN;i++) printf ("  %d   ", iv[i]);
  cout << endl;
  civ=IV;

  //int *ivv;
  ivv= new int[N+CN*CD];
  j=0;
  for(i=0;i<N+CN;i++)
    { if (iv[i]<COMPOSITE_BASE) ivv[j++]=iv[i];
    else 
      {for(int k=0;k<CD;k++)
	{
	  ivv[j++]=N+(iv[i]-COMPOSITE_BASE)*CD+k;
	
	}
       if (i<IV) civ+=CD-1;
      }				  
    }

  //Random a solution
  //int *Sol;
  //Sol= new int[N+CN*CD];
  Sol= (int*) malloc(sizeof(int)*(N+CN*CD));
  for(i=0;i<N+CN*CD;i++)
    {
      Sol[i]=(int) (D * ran2(Seed));    
      //cout << i << "=" << Sol[i] << endl;
    }

  
  i=0;
  for (var1=0; var1<(N-1); ++var1)
    for (var2=var1+1; var2<N; ++var2)
      CTarray[i++] = (var1 << 16) | var2;
  //cout << "Possible " << i << endl;
  //from here
  /* Select C constraints. */	
  int ICount=0,NCount=0,BCount=0,CICount=0;
  for (c=0; c<C; ++c)
    {
      /* Choose a random number between c and PossibleCTs - 1, inclusive. */

      r =  c + (int) (ran2(Seed) * (PossibleCTs - c)); 

      /* Swap elements [c] and [r]. */
      selectedCT = CTarray[r];
      CTarray[r] = CTarray[c];
      CTarray[c] = selectedCT;

      /* Broadcast the constraint. */
  
      CMatrix[(int)(CTarray[c] >> 16)][(int)(CTarray[c] & 0x0000FFFF)]=true;
      CMatrix[(int)(CTarray[c] & 0x0000FFFF)][(int)(CTarray[c] >> 16)]=true;
  
      /*------------------------------------------------------------ */	
      /* count remaining numbers of constraints of initial variables */
	      for (i=0;i<N+CN*CD;i++)
		{ 
		  if ((int)(CTarray[c] >> 16)==ivv[i]) {k=i;}
		  else if ((int)(CTarray[c] & 0x0000FFFF)==ivv[i]) {l=i; }
		}
	      if ((k<civ) && (l<civ)) 
		{ 
		  ICount++;
		  if((k>N)||(l>N)) CICount++;
		}
	      else if ((k>civ) && (l>civ)) NCount++;
	      else BCount++;     
    }

    
  // add composite variables here !!!
   free(CTarray);
   PPP= PP/CN;
   NN=N+CN*CD-CD;
   PossibleCTs = NN; // to add to the csp

   CTarray=new unsigned long[PossibleCTs];
   PossibleNGs = ALLEN_RELATIONS;


   for(int ii=0;ii<CN;ii++)
      for(j=0;j<CD;j++)
	{ 	  
	  k=0;
	  for (var1=0; var1<NN+CD; var1++)
	    { if ((var1 < (N +ii*CD)) || (var1 >=(N+ii*CD+CD)))
	      CTarray[k++] = (var1 << 16) | (N+ii*CD+j);
	    }
	  //cout <<"k=" <<k<<endl;
	  //if (PPP>PossibleCTs) PPP=PossibleCTs;
	  for (c=0; c<PPP; ++c)
	    { 
	      /* Choose a random number between c and PossibleCTs - 1, inclusive. */
	     do 
	       {
		 r =  c + (int) (ran2(Seed) * (PossibleCTs - c)); 
	       }
	     while (CMatrix[(int)(CTarray[r] >> 16)][(int)(CTarray[r] & 0x0000FFFF)]);

	      /* Swap elements [c] and [r]. */
	      selectedCT = CTarray[r];
	      CTarray[r] = CTarray[c];
	      CTarray[c] = selectedCT;

	      CMatrix[(int)(CTarray[c] >> 16)][(int)(CTarray[c] & 0x0000FFFF)]=true;
	      CMatrix[(int)(CTarray[c] & 0x0000FFFF)][(int)(CTarray[c] >> 16)]=true;
	      //cout << CMatrix[(int)(CTarray[c] >> 16)][(int)(CTarray[c] & 0x0000FFFF)] << endl;
	      /*------------------------------------------------------------ */	
	      /* count remaining numbers of constraints of initial variables */
	      int l;
	      for (i=0;i<N+CN*CD;i++)
		{ 
		  if ((int)(CTarray[c] >> 16)==ivv[i]) {k=i;}
		  else if ((int)(CTarray[c] & 0x0000FFFF)==ivv[i]) {l=i;}
		}
	      if ((k<civ) && (l<civ))
		{
		  ICount++; //cout << " ** "<<ICount << " ** ";
		  if((k>N)||(l>N)) CICount++;
		}
	      else if ((k>civ) && (l>civ)) NCount++;
	      else BCount++;
	    }	   
	}
    // Random Tightness T !! here
    NN=N+CN*CD;
    //SOPO *VN=new SOPO[NN] ;

    for(i=0;i<NN;i++)
      { 
	r =  MIN_DURATION + (int) (ran2(Seed) * (MAX_DURATION-MIN_DURATION)); 
	VN[i].Duration=r;

	step = 1 + (int) (ran2(Seed) * STEP);
	do
	{ 
	   r =  (int) (ran2(Seed) * (H-VN[i].Duration)); 
	} while ((r+D*step)+VN[i].Duration>H);

	VN[i].Begin=r;
	VN[i].End=(VN[i].Begin+(D-1)*step)+VN[i].Duration;
	VN[i].Step=step;
	for(j=0;j<i;j++)
	  { 

	    if (CMatrix[i][j])
	    { 
	      
	      if (SetRelations(T,D,RMatrix[i][j],VN[i],VN[j],i,j,Sat,Sol))
		{
		  RMatrix[j][i]=Inverse(RMatrix[i][j]);
		}
		else break;
	    }
	}  
	if(j<i)
	  i--;
      }

    // print SOPO and Relations
    printf("\nNumeric Constriants \n");
    for (i=0;i<NN;i++)
      printf("%3d = [%4d,%4d,%4d,%2d]\n",i,VN[i].Begin,VN[i].End,VN[i].Duration,VN[i].Step);
    cout << endl;
    // Random Activity constraints
  int d = (N+CN-IV)*ACTIVITY_PROB;
  printf ("Activity Constraints : %d",d);
  cout << endl;

  for (i=IV;i<N+CN;i++)
      { 
	ii=0;
	for(l=0;l<i;l++)
	  { if (iv[l]<COMPOSITE_BASE) 
	      {
		for(p=0;p<D;p++)
		    {
		      aa[ii].X=iv[l];
		      aa[ii].A=p;
		      aa[ii++].Y=iv[i];
		    }
	      } else
		{ 
		  dd=N+(iv[l]-COMPOSITE_BASE)*CD;
		  for(p=dd;p<dd+CD;p++)
		      {
			aa[ii].X=iv[l];
			aa[ii].A=p;
			aa[ii++].Y=iv[i];
			for(m=0;m<D;m++)
			  { 
			    aa[ii].X=p;
			    aa[ii].A=m;
			    aa[ii++].Y=iv[i];
			  }
		      }
		}
	  }

    for(c=0;c<ACTIVITY_PROB;c++)
    {  
      r = c + (int) ((ii-c) * ran2(Seed));
      a.X=aa[r].X;
      a.A=aa[r].A;
      a.Y=aa[r].Y;
      aa[r].X=aa[c].X;
      aa[r].A=aa[c].A;
      aa[r].Y=aa[c].Y;
      aa[c].X=a.X;
      aa[c].A=a.A;
      aa[c].Y=a.Y;
      printf("%5d =%3d -> %d\n", aa[c].X,aa[c].A, aa[c].Y);	
    } 
    j++;
  }
  delete  [] aa;
  /******************************************************/
  //random solution

  printf("\nBinary Constraints :: Disjunctive of ALLEN's Relations\n\n");
    for (i=0;i<NN;i++)
      for(j=0;j<i;j++)
	if (CMatrix[i][j])
	  { 
	    printf("%3d   %3d : %d ",i,j,RMatrix[i][j].count());
	    //cout << RMatrix[i][j]<<endl;
	    for(k=0;k<ALLEN_RELATIONS;k++)
	      if (RMatrix[i][j].test(k))
		PrintRelation(k);
	    cout << endl;
	  } 

    cout << "\n---------------------------------------------------------------\n";

    cout << "# Constraints of Initial CCCSP Instance       = " << int ((ICount-CICount)+CICount/CD) 
	 << " (" << ((ICount-CICount)+(CICount/CD))*100/(IV*(IV-1)/2) << "%)" << endl;
    cout << "# Constraints of CSP                          = " << C 
	 << " (" << C*100/(N*(N-1)/2) << "%)" << endl;
    cout << "# Constraints of Nonactive Variables          = " << NCount << endl;
    cout << "# Constriants of Init and Non Variables       = " << BCount << endl;
    cout << "# Constriants of CCCSP                        = " <<  C+PP/CD
         << " (" << (C+PP/CD)*100/((N+CN)*(N+CN-1)/2) << "%)" << endl;
    EndCSP();


    delete [] VN;
    delete [] iv;
    delete [] ivv;
    delete [] Sol;
    delete [] CTarray;
    for (int i=0;i<NN;i++)
      { 
	delete [] CMatrix[i];
        delete [] RMatrix[i];
      }
    delete [] CMatrix;
    delete [] RMatrix;
  return 1;
}

//------------------------FUNCTIONS-----------------------------
void StartCSP(int N, int D, int instance)
{
  printf("\nInstance %d\n", instance);
}

void AddConstraint(int var1, int var2)
{
  printf("\n%3d %3d : ", var1, var2);
}

void AddNogood(int val1, int val2)
{
  printf(" %d %d ", val1, val2);
}

void EndCSP()
{
  printf("\n");
}

void PrintRelation(int R)
{
  switch (R)
    {
    case P_R: cout << "P";break;
    case p_R: cout << "p";break;
    case S_R: cout << "S";break;
    case s_R: cout << "s";break;
    case F_R: cout << "F";break;
    case f_R: cout << "f";break;
    case M_R: cout << "M";break;
    case m_R: cout << "m";break;
    case O_R: cout << "O";break;
    case o_R: cout << "o";break;
    case D_R: cout << "D";break;
    case d_R: cout << "d";break;
    case E_R: cout << "E";break;
    };
}

struct INTERVAL AssociateInterval(int Value,struct SOPO Sopo)
{ 
  struct INTERVAL pair;
  pair.start=Sopo.Begin+Value*Sopo.Step;
  pair.end=pair.start+Sopo.Duration;
  return pair;
}

int Relation(struct INTERVAL X, struct INTERVAL Y)
{ 
  if (Satisfied(X,P_R,Y)) return P_R;
  else if (Satisfied(X,P_R,Y)) return P_R;
  else if (Satisfied(X,p_R,Y)) return p_R;
  else if (Satisfied(X,S_R,Y)) return S_R;
  else if (Satisfied(X,s_R,Y)) return s_R;
  else if (Satisfied(X,F_R,Y)) return F_R;  
  else if (Satisfied(X,f_R,Y)) return f_R;
  else if (Satisfied(X,M_R,Y)) return M_R;
  else if (Satisfied(X,m_R,Y)) return m_R;
  else if (Satisfied(X,O_R,Y)) return O_R;
  else if (Satisfied(X,o_R,Y)) return o_R;
  else if (Satisfied(X,D_R,Y)) return D_R;
  else if (Satisfied(X,d_R,Y)) return d_R;
  else if (Satisfied(X,E_R,Y)) return E_R;   
}

bool Satisfied(struct INTERVAL X,int R,struct INTERVAL Y)
{
// check if X and Y interval satisfy relation R
  switch (R)
    { case P_R: // P
      return ((X.start < Y.start) && (X.end < Y.start));
      case p_R: // p
	return Satisfied(Y,P_R,X);
      case S_R: // S
	return ((X.start==Y.start) && (X.end < Y.end));
      case s_R: // s
	return Satisfied(Y,S_R,X);
      case M_R: // M
	return (X.end==Y.start);
      case m_R: // m
	return Satisfied(Y,M_R,X);
      case O_R: // O
	return ((X.start<Y.start) && (X.end>Y.start && X.end <Y.end));
      case o_R: // o
	return Satisfied(Y,O_R,X);
      case D_R: // D
	return ((X.start>Y.start) && (X.end <Y.end));
      case d_R: // d
	return Satisfied(Y,D_R,X);
      case F_R: // F
	return ((X.start >Y.start) && (X.end ==Y.end));
      case f_R: // f
	return Satisfied(Y,F_R,X);
      case E_R: // E
	return ((X.start==Y.start) && (Y.end==X.end));
    }
}

void Num_Sym(bitset<ALLEN_RELATIONS> **C,int NN,struct SOPO *SOPOs)
{ int i,j,k;
 
  for (i=0;i<NN-1;i++)
    for (j=i+1;j<NN;j++)
      if (i!=j)
	{ // Case 1: only p can be hold
	  if (SOPOs[i].Begin > SOPOs[j].End)
	     { 
	       C[i][j].reset();
	       C[i][j].set(p_R);
	     }
	  // Case 2: only P can be hold
	  else if (SOPOs[i].End < SOPOs[j].Begin ) 
	       {
		 C[i][j].reset();
		 C[i][j].set(P_R);
	       } else 
		 { // Case 3: remove E
		   if ((SOPOs[i].Duration != SOPOs[j].Duration) || 
		       ((SOPOs[j].End-SOPOs[i].Begin) < SOPOs[i].Duration) ||
		       ((SOPOs[i].End-SOPOs[j].Begin) < SOPOs[i].Duration))
		      {
			C[i][j].reset(E_R);

			// Case 4: remove S,F,D
			if (SOPOs[i].Duration > SOPOs[j].Duration)
			  {  
			    C[i][j].reset(S_R);
			    C[i][j].reset(F_R);
			    C[i][j].reset(D_R);
			    
			  } // Case 5: remove s,f,d
			else if (SOPOs[i].Duration < SOPOs[j].Duration)
		          { C[i][j].reset(s_R);
			    C[i][j].reset(f_R);
			    C[i][j].reset(d_R);
		          }
		      }
		   else if (SOPOs[i].Duration==SOPOs[j].Duration)
		      { C[i][j].reset(D_R); // remove DdSsFf
		        C[i][j].reset(d_R);
			C[i][j].reset(S_R);
			C[i][j].reset(s_R);
			C[i][j].reset(F_R);
			C[i][j].reset(f_R);
		      }
		   // Case 6: remove M,P
		    if (SOPOs[i].Begin+SOPOs[i].Duration > SOPOs[j].End-SOPOs[j].Duration)
		       { C[i][j].reset(M_R); // remove M
		         C[i][j].reset(P_R); // remove P
		       }
		       // Case 7: remove m,p
		    if (SOPOs[i].End-SOPOs[i].Duration < SOPOs[j].Begin+SOPOs[j].Duration)
			 {
			    C[i][j].reset(m_R); // remove m
			    C[i][j].reset(p_R); // remove p
			 }
		   // Case 8: remove S,s,O,d
		   if (SOPOs[i].Begin > SOPOs[j].End-SOPOs[j].Duration)
		     { 
			C[i][j].reset(S_R); // remove S
		        C[i][j].reset(s_R); // remove s
			C[i][j].reset(O_R); // remove O
			C[i][j].reset(d_R); // remove d
		      }
		   // Case 9: remove F,f,D
		   if (SOPOs[i].Begin+SOPOs[i].Duration > SOPOs[j].End)
		      { C[i][j].reset(F_R); // remove F
		        C[i][j].reset(f_R); // remove f
			C[i][j].reset(D_R);  // remove D
		      }
		   //  remove S
		      if ((SOPOs[i].End - SOPOs[i].Duration) < SOPOs[j].Begin) 
		         C[i][j].reset(S_R); // remove S
		          

		   // Case 12: remove F,f
		   if (SOPOs[i].End<SOPOs[j].Begin + SOPOs[j].Duration)
		      { C[i][j].reset(F_R); // remove F
			C[i][j].reset(f_R); // remove f
		      }  
		   
		   }
		   
	  C[j][i]=Inverse(C[i][j]);
	  //cout << i << "-" << j << " " << C[i][j] << endl;
	  //cout << j << "-" << i << " " << C[j][i] << endl;
	}// end if
}//end function

bitset<ALLEN_RELATIONS> Inverse(bitset<ALLEN_RELATIONS> Rel)
{int i;
 bitset<ALLEN_RELATIONS> tmp;
 tmp.reset();
 for(i=0;i<ALLEN_RELATIONS-1;i++)
   if (Rel.test(i))
     if (i % 2) 
       tmp.set(i-1);
     else tmp.set(i+1); 
 if (Rel.test(E_R)) tmp.set(E_R);
 return tmp;
   
}

void ConstraintTightness(struct SOPO *SOPOs,int NN,int DZ)
{ 
  int i,j,k;
  int d1,d2;
  int Tightness[ALLEN_RELATIONS];

  struct INTERVAL X,Y;
  for(i=0;i<NN;i++)
    for(j=i+1;j<NN-1;j++)
      { for(k=0;k<ALLEN_RELATIONS;k++) Tightness[k]=0;
	for(d1=0;d1<DZ;d1++)
	{ 
	  X=AssociateInterval(d1,SOPOs[i]);
	  for(d2=0;d2<DZ;d2++)
	    {
	      Y=AssociateInterval(d2,SOPOs[j]);
	      Tightness[Relation(X,Y)]++;
	    }    

	}
	printf("%3d %3d : ", i,j );
	for(k=0;k<ALLEN_RELATIONS;k++) 
	  { 
	    PrintRelation(k);
	    printf ( "=  %3d ", Tightness[k]);
	  }
	cout << endl;
      }

}

void CountConstraint(struct SOPO *SOPOs,int NN,int DZ,int i,int j, int *Tightness)
{ 
  int k;
  int d1,d2;

  struct INTERVAL X,Y;
  for(k=0;k<ALLEN_RELATIONS;k++) Tightness[k]=0;
  for(d1=0;d1<DZ;d1++)
	{ 
	  X=AssociateInterval(d1,SOPOs[i]);
	  for(d2=0;d2<DZ;d2++)
	    {
	      Y=AssociateInterval(d2,SOPOs[j]);
	      Tightness[Relation(X,Y)]++;
	    }    
	}
}


bool SetRelations(int T,int DZ,bitset<ALLEN_RELATIONS> &RMatrix, struct SOPO XSOPO,struct SOPO YSOPO,int x,int y, bool Sat, int *Sol)
{
  int k;
  int d1,d2,i,ccount,j;
  int Tightness[ALLEN_RELATIONS];
  struct INTERVAL X,Y;
  int **DD;
  
  DD= (int**) malloc(sizeof(int*)*(ALLEN_RELATIONS+1));
  for(i=0;i<ALLEN_RELATIONS+1;i++)
    DD[i]=(int*) malloc(sizeof(int)*DZ);
  for(i=0;i<ALLEN_RELATIONS+1;i++)
    for(j=0;j<DZ;j++)
      DD[i][j]=0;

  RMatrix.set();
  for(k=0;k<ALLEN_RELATIONS;k++) 
    { 
      Tightness[k]=0;
    }

  for(d1=0;d1<DZ;d1++)
	{ 
	  X=AssociateInterval(d1,XSOPO);
	  for(d2=0;d2<DZ;d2++)
	    {
	      Y=AssociateInterval(d2,YSOPO);
	      Tightness[Relation(X,Y)]++;
	      DD[Relation(X,Y)][d1]++;
	    }    
	}
    
  int avg= (int) (ceil(T*TIGHTNESS_RANGE));
  int ccout;
  RMatrix.reset();

  int yyy=f(0,T + avg,Tightness,RMatrix);
  int e;
  //cout << "after "<< endl;
  if (yyy>=(T-avg))
    { 
      
      if (Sat)
	{ for(i=0;i<ALLEN_RELATIONS;i++)
	  { //cout << "#domain=" << DD[i][Sol[ii]] << endl;
	    if (RMatrix.test(i) && (Satisfied(AssociateInterval(Sol[x],XSOPO),i,AssociateInterval(Sol[y],YSOPO))))
	      {
		  for (int e=0;e<ALLEN_RELATIONS+1;e++)
		    { 
		      delete [] DD[e];
		    }
		  delete [] DD;
		return true;
	      }
	  }
	   for (int e=0;e<ALLEN_RELATIONS+1;e++)
		    { 
		      delete [] DD[e];
		    }
		  delete [] DD;
	    return false;
	}
      else
	{ 
	  for (int e=0;e<ALLEN_RELATIONS+1;e++)
		    { 
		      delete [] DD[e];
		    }
		  delete [] DD;
	  return true;
       }
    }
  else 
   {
     for (int e=0;e<ALLEN_RELATIONS+1;e++)
		    { 
		      delete [] DD[e];
		    }
		  delete [] DD; 
      return false;  
   }
}


//calculate the best compatible relations that close to T/+-/TIGHTNESS_RANGE*T
int f(int nn,int yy, int Tightness[ALLEN_RELATIONS],bitset<ALLEN_RELATIONS> &RMatrix)
{ int Yes,No,Max;
  bitset<ALLEN_RELATIONS> NTRM, YTRM;  
  NTRM.reset();
  YTRM.reset();
  bool YN;
  if(nn==ALLEN_RELATIONS-1) 
    {
    if  ((yy >= Tightness[nn]) && (Tightness[nn]>0)) 
      { 
	return Tightness[nn];
      }
    else
      { 
	return 0;
      }
  } 
  else
    { 
      if ((yy >= Tightness[nn])&&(Tightness[nn]>0))
	{ 
	  No=f(nn+1,yy,Tightness,NTRM);

	  Yes=f(nn+1,yy-Tightness[nn],Tightness,YTRM)+Tightness[nn];

	  if (No>=Yes)
	    { NTRM.reset(nn);
	      Max=No;
	      YN=false;
	      //RMatrix.reset(nn);
	      //return No;
	    }
	  else 
	    { YTRM.set(nn);
	      Max=Yes;
	      YN=true;
	    }
	  
	}
      else 
	{ 
	  RMatrix.reset(nn); // reset Tightness;
	  return f(nn+1,yy,Tightness,RMatrix);
	}
      if (YN) RMatrix=RMatrix|YTRM;
      else RMatrix=RMatrix|NTRM;
      return Max;     
    } 
}

int max(int X,int Y)
{ 
  if (X>=Y) 
    return X;
  else 
    return Y; 
}


/*********************************************************************
  3. This random number generator is from William H. Press, et al.,
     _Numerical Recipes in C_, Second Ed. with corrections (1994), 
     p. 282.  This excellent book is available through the
     WWW at http://nr.harvard.edu/nr/bookc.html.
     The specific section concerning ran2, Section 7.1, is in
     http://cfatab.harvard.edu/nr/bookc/c7-1.ps
*********************************************************************/

#define IM1   2147483563
#define IM2   2147483399
#define AM    (1.0/IM1)
#define IMM1  (IM1-1)
#define IA1   40014
#define IA2   40692
#define IQ1   53668
#define IQ2   52774
#define IR1   12211
#define IR2   3791
#define NTAB  32
#define NDIV  (1+IMM1/NTAB)
#define EPS   1.2e-7
#define RNMX  (1.0 - EPS)

/* ran2() - Return a random floating point value between 0.0 and
   1.0 exclusive.  If idum is negative, a new series starts (and
   idum is made positive so that subsequent calls using an unchanged
   idum will continue in the same sequence). */

float ran2(long *idum)
{
  int j;
  long k;
  static long idum2 = 123456789;
  static long iy = 0;
  static long iv[NTAB];
  float temp;

  if (*idum <= 0) {                             /* initialize */
    if (-(*idum) < 1)                           /* prevent idum == 0 */
      *idum = 1;
    else
      *idum = -(*idum);                         /* make idum positive */
    idum2 = (*idum);
    for (j = NTAB + 7; j >= 0; j--) {           /* load the shuffle table 
*/
      k = (*idum) / IQ1;
      *idum = IA1 * (*idum - k*IQ1) - k*IR1;
      if (*idum < 0)
        *idum += IM1;
      if (j < NTAB)
	iv[j] = *idum;
    }
    iy = iv[0];
  }
      
  k = (*idum) / IQ1;
  *idum = IA1 * (*idum - k*IQ1) - k*IR1;
  if (*idum < 0)
    *idum += IM1;
  k = idum2/IQ2;
  idum2 = IA2 * (idum2 - k*IQ2) - k*IR2;
  if (idum2 < 0)
    idum2 += IM2;
  j = iy / NDIV;
  iy = iv[j] - idum2;
  iv[j] = *idum;
  if (iy < 1)
    iy += IMM1;
  if ((temp = AM * iy) > RNMX)
    return RNMX;                                /* avoid endpoint */
  else
    return temp;
}
