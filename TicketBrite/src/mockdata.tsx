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
            tickets: [
                {
                    name: "Staanplaatsen",
                    price: 45
                },
                {
                    name: "Zitplaatsen",
                    price: 65
                }
            ]
        }
    ]
}

export default events;