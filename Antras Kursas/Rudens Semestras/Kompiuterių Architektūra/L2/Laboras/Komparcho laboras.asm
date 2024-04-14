;
; suskaiciuoti     /   |x+b|             , kai c=a*x
;              y = |   a^2-3*b           , kai c<a*x
;                  \   ](2*c-a)/(c+a*x)[ , kai c>a*x<0
; skaiciai su zenklu  
; Duomenys a - w, b - w, c - w, x - b, y - b  
; X ir Y - Masyvai
             
;
; suskaiciuoti     /   (b+c^2)/(x-b)    , kai x>b 
;              y = |   7*a-x            , kai x=b
;                  \   |c|+2*a          , kai x<b
; skaiciai su zenklu
; Duomenys a - w, b - b, c - w, x - w, y - b  
 
 
; a	DW  2  ;   10000; perpildymo situacijai 
;b	DW -2
;c	DW 8
;x	DB -9, -1,-2,-8,-4,12,9,45,6

stekas  SEGMENT STACK
	DB 256 DUP(0)
stekas  ENDS

duom    SEGMENT 
a	DW  75  ;   10000; perpildymo situacijai 
b	DW 100
c	DW 50
x	DB -50, 120, 50, 0, -49, -51, -55, -60, -100, -120
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
	
	ADD ax, c
	MOV bx, 0
	CMP ax, bx
	JG f2
	JL f3 
f1:	MOV ax, c
    MOV bx, 2
    
	IMUL bx	; ax*bx = 2c
	JO kl1 
	XCHG ax, bx
	MOV al, x[si]
	CBW 
	CMP ax, 0
	JG p1
	JL p2
	
p1:	
	ADD ax, bx
	JO kl1
	JMP re  
	
p2:	NEG ax
    ADD ax, bx
	JO kl1
	JMP re  
	
	
	
	
		
f2:	
    MOV al, x[si] 
    CBW
    MOV bx, 2
    IMUL bx
    JO kl1
    XCHG ax, bx

    MOV ax, c
	IMUL c
	JO kl1  ; sandauga netilpo i ax 
	XCHG ax, bx
	SUB ax, bx
	JO kl1
	JMP re 
	   
f3:	MOV al, x[si] 
    CBW
    ADD ax, c 
    JO kl1
    CMP ax, 0
    JE kl2
    XCHG ax, bx
    MOV ax, b
    ADD ax, a  
    JO kl1 
    XOR dx, dx
    IDIV bx
    JO kl1
    JMP re
    


	NEG bx
mod:	ADD ax, bx ;2a+|c|
	JO kl1
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

