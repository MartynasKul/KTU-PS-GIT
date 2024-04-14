stek segment
     dw 128 dup(0)
	 ends

duom segment 
    
    a db 45, 12, 46, 32
pran db "Labas, PASAULI!", 0Dh,0Ah, 24h,
	 ends

	 assume cs:kod, ds: duom
kod  segment
pradzia:
     mov ax, duom ; segmento registro uzkrovimas
     mov ds, ax

     lea dx, pran ; pranesimo adreso uzkrovimas
     mov ah, 09h ; funkcija "isvesti pranesima i ekrana"
     int 21h ; iskvieciamas programinis MS DOS pertraukimas
        
     mov ah, 0    ; pauze, funkcija "ivesti simboli"
     int 16h      ; 

     mov ax, 4c00h; funkcija "baigti programos darba, grizti i OS"
     int 21h  

	 ends
	 end pradzia