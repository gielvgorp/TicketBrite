describe('Events overview page', () => {
    it('Ticket selector should add ticket when clicking plus button', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get("[data-test='ticket-selector-container'] [data-test='ticket-selector']:first-child").as("TicketSelector").find("span").should("have.text", "0");

        cy.get('@TicketSelector').first().find("button").last().click();

        cy.intercept('GET', '**/get-events').as('getEvents');

        cy.wait('@getEvents').then((interception) => {
            expect(interception.response.statusCode).to.eq(201)
        });

        cy.get('@TicketSelector').first().find("span").should("have.text", "1");
    });

    it('Ticket selector button should disable at 10 tickets', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get("[data-test='ticket-selector-container'] [data-test='ticket-selector']:first-child").as("TicketSelector").find("span").should("have.text", "0");

        for (let i = 0; i < 10; i++) {
            cy.get('@TicketSelector').first().find("button").last().click();
        }

        cy.get('@TicketSelector').first().find("button").last().should("be.disabled");
    });

    it('Login warning should pop up when user wants to add ticket to shoppingcart', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get(".sign-in-container a").should("have.text", "Inloggen / Registreren");

        cy.get('._ticketSelector_1r6ku_1:first-child ._selectTicket_1r6ku_37').first().find("button").last().click();


        cy.get('._sideBar_13834_29 button.btn-success').first().click();

        cy.get(".modal").should("have.class", "show");
    });
});
