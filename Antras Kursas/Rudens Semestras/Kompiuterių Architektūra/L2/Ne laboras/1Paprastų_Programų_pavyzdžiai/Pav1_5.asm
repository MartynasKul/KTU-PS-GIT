;
; suskaiciuoti     /   x+a, kai b+c>5; , visi skaiciai zodzio ilgio su zenklu
;              y = |   x-a, kai b+c=5;
;                  \   x+3, kai b+x<5.
stack segment
    dw   128  dup(0)
ends

data segment
  
a 	DW 20
b 	DW -11
c 	DW 222
x 	DW 255
y 	DW ?


pran    db      "Suskaiciavau...", 0Dh,0Ah, 24h
ends
 
assume cs:code, ds: data
code segment
pradzia:
        mov     ax, data      ; segmento registro uzkrovimas
        mov     ds, ax
        
	MOV dx , b
	ADD dx , c
	CMP dx , 5
	JG a1
	JE a2
	MOV ax , x
	ADD ax , 3
	JMP p
a1: 	MOV ax , x
	ADD ax , x
	JMP P
a2: 	MOV ax , x
	SUB ax , x
P: 	MOV y, ax

                
        lea     dx, pran  ; pranesimo adreso uzkrovimas
        mov     ah, 09h  ; funkcija "isvesti pranesima 5 ekrana"
        int     21h      ; iskvieciamas programinis MS DOS pertraukimas
        
        mov     ah, 0    ; Funkcija "ivesti simboli"
        int     16h      ; 

        mov     ax, 4c00h            ;funkcija "baigti programos darba, grizti i OS"
        int     21h  

        ends

        end pradzia
