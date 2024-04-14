;
; suskaiciuoti X =A/C + (A * 2 + B * C)/(D - 3), visi skaiciai zodzio ilgio su zenklu
;
stack segment
    dw   128  dup(0)
ends

data segment
  
a	DW -255
b 	DW 25
c 	DW -250
d 	DW 222
x 	DW ?
pran    db      "Suskaiciavau...", 0Dh,0Ah, 24h
ends
 
assume cs:code, ds: data
code segment
pradzia:
        mov     ax, data      ; segmento registro uzkrovimas
        mov     ds, ax
        
        MOV ax , a 	; ax:=a
	CWD		; dx:ax:=ax
	IDIV c 		; ax:=a/c
	MOV x, ax 		; x:=a/c
	MOV ax , 2 	; ax :=2
	IMUL a ; 		dx:ax:=a*2
	MOV bx , dx
	MOV cx , ax 	; bx:cx:=a*2
	MOV ax , b
	IMUL c 		; dx:ax:=b*c
	ADD ax , cx
	ADC dx , bx 	;dx:ax:=b*c+a*2
	MOV cx , d
	SUB cx , 3 		; cx:=d-3
	IDIV cx 		; ax :=(b*c+a *2)/(d -3)
	ADD x, ax 		; x:=a/c+(b*c+a *2)/(d -3)
                
        lea     dx, pran  ; pranesimo adreso uzkrovimas
        mov     ah, 09h  ; funkcija "isvesti pranesima 5 ekrana"
        int     21h      ; iskvieciamas programinis MS DOS pertraukimas
        
        mov     ah, 0    ; Funkcija "ivesti simboli"
        int     16h      ; 

        mov     ax, 4c00h            ;funkcija "baigti programos darba, grizti i OS"
        int     21h  

        ends

        end pradzia
