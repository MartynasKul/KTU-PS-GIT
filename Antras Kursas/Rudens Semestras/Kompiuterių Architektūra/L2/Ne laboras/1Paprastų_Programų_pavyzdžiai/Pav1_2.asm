;
; suskaiciuoti x=a+b-c
;
data segment
  
a       dw      10
b       dw      20
C       dw      15
X       dw      ?
pran    db      "Suskaiciavau...", 0Dh,0Ah, 24h
ends

stack segment
    dw   128  dup(0)
ends

assume cs:code, ds: data
code segment
pradzia:
        mov     ax, data      ; segmento registro uzkrovimas
        mov     ds, ax
        
        mov     ax, a
        mov     bx, b
        add     ax, bx
        sub     ax, c
        mov     x, ax
                
        lea     dx, pran  ; pranesimo adreso uzkrovimas
        mov     ah, 09h  ; funkcija "isvesti pranesima 5 ekrana"
        int     21h      ; iskvieciamas programinis MS DOS pertraukimas
        
        mov     ah, 0    ; Funkcija "ivesti simboli"
        int     16h      ; 

        mov     ax, 4c00h            ;funkcija "baigti programos darba, grizti i OS"
        int     21h  

        ends

        end pradzia
