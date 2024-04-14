;  apskaiciuoti masyvo A(n) elementu suma
stack segment
    dw   128  dup(0)
ends

data segment
  
A 	DB -10, -8, 2, 4, 5, 7, -11
N = ($-A)
S 	DB ?

pran    db      "Suskaiciavau...", 0Dh,0Ah, 24h
ends
 
assume cs:code, ds: data
code segment
pradzia:
        mov     ax, data      ; segmento registro uzkrovimas
        mov     ds, ax
        
	MOV ah , 0 
	MOV si , 0
	MOV cx , N
	JCXZ pab
c_pr: 	ADD ah , a[si]
	INC si
	LOOP c_pr
pab: 	MOV s, ah


                
        lea     dx, pran  ; pranesimo adreso uzkrovimas
        mov     ah, 09h  ; funkcija "isvesti pranesima 5 ekrana"
        int     21h      ; iskvieciamas programinis MS DOS pertraukimas
        
        mov     ah, 0    ; Funkcija "ivesti simboli"
        int     16h      ; 

        mov     ax, 4c00h            ;funkcija "baigti programos darba, grizti i OS"
        int     21h  

        ends

        end pradzia
