# 1. Feladat kérdéseire válasz

Mit biztosít a Strategy a DI mintával kombinálva a labor példa keretében,
mik az együttes alkalmazásuk előnyei?

Lehetővé teszi, hogy könnyedén fel lehessen cserélni az Anonymizer három
megoldási menetét. Ezeket könnyen össze lehet vetni, továbbá a kód könnyebben
bővíthető, és mivel minden megoldási menetnek hasonló alapjai vannak, ezért
könnyen újrafelhasználható a kód

Mit jelent az, hogy a Strategy minta alkalmazásával az Open/Closed elv
megvalósul a megoldásban?

A Strategy minta nyitott a bővítésre és zárt a módosításra, a labor példa
esetében két Progress fajta van, a Percent -és a SimpleProgress. Ezek mellett
tovább bővíthető más Progress mintával anélkül, hogy a már meglévő Progress-ek
működését befolyásolná.
