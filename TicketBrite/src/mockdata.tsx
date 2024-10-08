function events(){
    return [
        {
            id: 1,
            eventName: "Snelle: LIVE!",
            artist: "Snelle",
            eventDate: new Date("01-10-2024 19:15"),
            eventLocation: "Jaarbeurs, Utrecht",
            eventAge: 12,
            category: "Muziek",
            image: "https://www.agentsafterall.nl/wp-content/uploads/Naamloos-1-header-1-1600x740.jpg",
            description: "Op 1 oktober 2024 staat Snelle in de Johan Cruijff ArenA voor een spectaculaire liveshow die je niet wilt missen! Met zijn grootste hits, waaronder 'Smoorverliefd', 'Reünie' en 'Blijven Slapen', brengt hij een avond vol meeslepende muziek, persoonlijke verhalen en een onvergetelijke sfeer. Snelle neemt je mee op een muzikale reis met zijn kenmerkende mix van pop en rap, aangevuld met een indrukwekkende lichtshow en special effects. Bereid je voor op een avond vol emotie, energie en een verbondenheid met duizenden fans. Dit concert is dé kans om Snelle in topvorm te zien schitteren!",
            tickets: [
                {
                    name: "Staanplaatsen",
                    price: 45
                },
                {
                    name: "Zitplaatsen",
                    price: 65
                },
                {
                    name: "VIP Area",
                    price: 65
                }
            ]
        },
        {
            id: 2,
            eventName: "Snollebollekes",
            artist: "Snollebollekes",
            eventDate: new Date("12-10-2024 19:15"),
            eventLocation: "Johan Cruijjf ArenA, Amsterdam",
            eventAge: 18,
            category: "Muziek",
            image: "https://www.voxweb.nl/wp-content/uploads/2021/10/Nijmeegs-Studentenorkest-Snollebollekes.jpeg",
            description: "Op 12 oktober 2024 neemt Snollebollekes de Johan Cruijff ArenA over voor een onvergetelijke avond vol energie en feestgedruis! Bereid je voor op een avond waar je niet stil kunt blijven staan, met de grootste meezingers, knotsgekke polonaises en een zee van rood, geel en blauw. Dit concert belooft een explosie van gezelligheid en plezier te worden, waarbij de Snollebollekes hun grootste hits ten gehore zullen brengen. Of je nu links, rechts, of in de lucht wilt springen, dit is hét evenement dat je niet mag missen. Kom samen met duizenden fans en beleef het feest van je leven!",
            tickets: [
                {
                    name: "Staanplaatsen",
                    price: 45
                },
                {
                    name: "Zitplaatsen",
                    price: 65
                },
                {
                    name: "Golden Circle",
                    price: 150
                }
            ]
        }
    ]
}

export default events;