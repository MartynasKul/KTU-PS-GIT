;
; suskaiciuoti     /   |x+b|             , kai c=a*x
;              y = |   a^2-3*b           , kai c<a*x
;                  \   ](2*c-a)/(c+a*x)[ , kai c>a*x<0
; skaiciai su zenklu  
; Duomenys a - w, b - w, c - w, x - b, y - b  
; X ir Y - Masyvai

      
           
           

stekas  SEGMENT STACK
	DB 256 DUP(0)
stekas  ENDS

duom    SEGMENT 
a	DW 50  ;   10000; perpildymo situacijai 
b	DW -100
c	DW 25000
x	DB 1
kiek	= ($-x)
y	DB kiek dup(0AAh)     
isvb	DB 'x=',6 dup (?), ' y=',6 dup (?), 0Dh, 0Ah, '$'
perp	DB 'Perpildymas', 0Dh, 0Ah, '$'
daln	DB 'Dalyba is nulio', 0Dh, 0Ah, '$'
netb	DB 'Netelpa i baita', 0Dh, 0Ah, '$'
spausk  DB 'Skaiciavimas baigtas, spausk bet kuri klavisa,', 0Dh, 0Ah, '$' 
duom    ENDS

prog    SEGMENT
	assume ss:stekas, ds:duom, cs:prog
pr:	MOV ax, duom
	MOV ds, ax
	XOR si, si      ; (suma mod 2) si = 0
	XOR di, di      ; di = 0
c_pr:   MOV cx, kiek
        JCXZ pab
cikl:
	MOV al, x[si]  
	CBW
	MOV bx, c  
	IMUL a
	JO kl1
	CMP ax, bx 
	
	JG f2  ; c<ax
	JL f3  ; c>ax

f1:	          ; c=ax
    MOV al, x[si]
    CBW
    MOV bx, b   
    ADD ax, bx
      
    CMP ax, 0

	JG p1
	JL p2
	
p1:	  
	JMP re  
	
p2:	
    NEG ax 
	JMP re  
		
f2:	
	MOV ax, a
	IMUL ax
	JO kl1
	XCHG ax, bx
	MOV ax, b
	MOV dx, 3
	IMUL dx 	
	JO kl1
	SUB bx, ax
	XCHG ax, bx
	JO kl1  	
	JMP re 
	   
f3:	


    MOV ax, c
    MOV bx, 2
    IMUL bx
    JO kl1
    SUB ax,a
    JO kl1
    XCHG ax, dx  ; virsus perkeliamas i dx
    
    MOV al, x[si] 
    CBW  
    XCHG bx, dx
    IMUL a
    JO kl1 
    XCHG bx, dx
    MOV bx, c
    ADD ax, bx
    JO kl1
    XCHG bx, ax
    XCHG ax, dx

    XOR dx,dx
    CMP bx, 0
    JZ kl2 ; tikrina ar apacia 0    
    IDIV bx 
    JMP re


re:	
	CMP al, 0
	JGE teigr 
	CMP ah, 0FFh  ; jei neig. rezultatas
	JE  ger
	JMP kl3
teigr:  CMP ah, 0     ;jei teig. rezultatas
        JE ger	
	JMP kl3
ger:	MOV y[di], al
	INC si
	
	INC di
	LOOP cikl
pab:	    
	    
;rezultatu isvedimas i ekrana	            
;============================
	XOR si, si
	XOR di, di 
        MOV cx, kiek
        JCXZ is_pab
is_cikl:
	MOV al, x[si]  ; isvedamas skaicius x yra ax reg.
	CBW 
	PUSH ax
	MOV bx, offset isvb+2  
	PUSH bx
	CALL binasc
	MOV al, y[di]
	CBW		; isvedamas skaicius y yra ax reg. 
	PUSH ax
	MOV bx, offset isvb+11 
	PUSH bx
	CALL binasc
	  
	MOV dx, offset isvb
	MOV ah, 9h
	INT 21h 
;============================    
	INC si
	INC di
	LOOP is_cikl
is_pab:	
;===== PAUZE ===================  
;===== paspausti bet kuri klavisa ===
	LEA dx, spausk
	MOV ah, 9
	INT 21h
	MOV ah, 0 
        INT 16h 
;============================        
        MOV ah, 4Ch   ; programos pabaiga, grizti i OS
	INT 21h
;============================	
	
kl1:	LEA dx, perp
	MOV ah, 9
	INT 21h
	XOR al, al
	JMP ger
kl2:	LEA dx, daln
	MOV ah, 9
	INT 21h
	XOR al, al
	JMP ger
kl3:	LEA dx, netb
	MOV ah, 9
	INT 21h
	XOR al, al
	JMP ger

; skaiciu vercia i desimtaine sist. ir issaugo
; ASCII kode. Parametrai perduodami per steka
; Pirmasis parametras ([bp+6])- verciamas skaicius
; Antrasis parametras ([bp+4])- vieta rezultatui

binasc	PROC NEAR   
	PUSH bp
	MOV bp, sp
; naudojamu registru issaugojimas
	PUSHA  
; rezultato eilute uzpildome tarpais
	MOV cx, 6   
	MOV bx, [bp+4]
tarp:	MOV byte ptr[bx], ' '
	INC bx
	LOOP tarp
; skaicius paruosiamas dalybai is 10   
	MOV ax, [bp+6]
	MOV si, 10
	CMP ax, 0
	JGE val
; verciamas skaicius yra neigiamas
	NEG ax
val:	XOR dx, dx
	DIV si
;  gauta liekana verciame i ASCII koda
	ADD dx, '0'   ; galima--> ADD dx, 30h
;  irasome skaitmeni i eilutes pabaiga
	DEC bx
	MOV [bx], dl
; skaiciuojame pervestu simboliu kieki
	INC cx
; ar dar reikia kartoti dalyba?
	CMP ax, 0
	JNZ val
; gautas rezultatas. Uzrasome zenkla
;	pop ax  
	MOV ax, [bp+6]
	CMP ax,0
	JNS teig
; buvo neigiamas skaicius, uzrasome -
	DEC bx
	MOV byte ptr[bx], '-'
	INC cx
	JMP vepab
; buvo teigiamas skaicius, uzrasau +
teig:	DEC bx
	MOV byte ptr[bx], '+'
	INC cx
vepab: 	
	POPA  
	POP bp
	RET
binasc	ENDP    
prog    ENDS 
        END pr

