;
; suskaiciuoti:
;      
;              y = (b+c)/(x^2)  , kai x>b; 
;              y = 7a-b+x       , kai x<=b   
;
; skaiciai su zenklu
; Duomenys a - w, b - b, c - w, x - w, y - b  

stekas  SEGMENT STACK
	DB 256 DUP(0)
stekas  ENDS

duom    SEGMENT 
a	DW  2
b	DB -2
c	DW 8
x	DW -1,-2,-4,12,9,45,6
kiek	= ($-x)/2
y	DB kiek dup(0AAh)     
isvb	DB 'x=',6 dup (?), ' y=',6 dup (?), 0Dh, 0Ah, '$'
perp	DB 'Perpildymas', 0Dh, 0Ah, '$'
daln	DB 'Dalyba is nulio', 0Dh, 0Ah, '$'
netb	DB 'Netelpa i baita', 0Dh, 0Ah, '$'
spausk  DB 'Skaiciavimas baigtas, spausk bet kuri klavisa,', 0Dh, 0Ah, '$' 
duom    ENDS

prog    SEGMENT
pr:	MOV ax, duom
	MOV ds, ax
	XOR si, si      ; (suma mod 2) si = 0
	XOR di, di      ; di = 0         
	
c_pr:               
    MOV cx, kiek
    JCXZ pab      
    
cikl:               ; pagrindinis ciklas    
;tikriname salyga 
	MOV al, b
	CBW   
	CMP x[si], ax
	JLE  f2     
	
;skaiciuojame (b+c)/(x^2)
f1:	MOV al, b
    CBW
	ADD ax, c
	JO kl1          ; suma netilpo i ax 
	PUSH ax
	MOV ax, x[si]
    IMUL ax      
    JO kl1          ; sandauga netilpo i ax
	MOV bx, ax 
	POP ax
	CWD 
	IDIV bx	        ; rezultatas - ax registre
	JMP re  
	
;skaiciuojame 7a-b+x		
f2:	MOV ax, 7
	IMUL a
	JO kl1          ; sandauga netilpo i ax 
	MOV bx, ax
	MOV al, b
	CBW
	SUB bx, ax
	JO kl1         
	ADD bx, x[si]   
	MOV ax, bx
	JO kl1
	         
re:	
	CMP al, 0
	JGE teigr 
	CMP ah, 0FFh  ; jei neig. rezultatas
	JE  ger
	JMP kl3         
	
teigr:  
    CMP ah, 0     ;jei teig. rezultatas
    JE ger	
	JMP kl3         

;rezultatu irasymas	
ger:	
    MOV y[di], al
	INC si
	INC si
	INC di
	LOOP cikl	           
	
;rezultatu isvedimas i ekrana	            
;============================  
pab:
	XOR si, si
	XOR di, di 
    MOV cx, kiek
    JCXZ is_pab  
    
is_cikl:
	MOV ax, x[si]  ; isvedamas skaicius x yra ax reg. 
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
	
kl1:
    LEA dx, perp
	MOV ah, 9
	INT 21h
	XOR al, al
	JMP ger
kl2:
    LEA dx, daln
	MOV ah, 9
	INT 21h
	XOR al, al
	JMP ger
kl3:
    LEA dx, netb
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
tarp:
    MOV byte ptr[bx], ' '
	INC bx
	LOOP tarp
; skaicius paruosiamas dalybai is 10   
	MOV ax, [bp+6]
	MOV si, 10
	CMP ax, 0
	JGE val
; verciamas skaicius yra neigiamas
	NEG ax
val:
    XOR dx, dx
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
teig:
    DEC bx
	MOV byte ptr[bx], '+'
	INC cx
vepab: 	
	POPA  
	POP bp
	RET
binasc	ENDP    
prog    ENDS 
        END pr


