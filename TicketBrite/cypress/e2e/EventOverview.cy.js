describe('Events overview page', () => {
    it('Ticket selector should add ticket when clicking plus button', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get('._ticketSelector_1r6ku_1:first-child ._selectTicket_1r6ku_37').first().as("TicketSelector").find("span").should("have.text", "0");

        cy.get('@TicketSelector').first().find("button").last().click();

        cy.get('@TicketSelector').first().find("span").should("have.text", "1");
    });

    it('Ticket selector button should disable at 10 tickets', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get('._ticketSelector_1r6ku_1:first-child ._selectTicket_1r6ku_37').first().as("TicketSelector").find("span").should("have.text", "0");

        for (let i = 0; i < 10; i++) {
            cy.get('@TicketSelector').first().find("button").last().click();
        }

        cy.get('@TicketSelector').first().find("button").last().should("be.disabled");
    });

    it('Login warning should pop up when user wants to add ticket to shoppingcart', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get("._signInContainer_11k3u_65 a").should("have.text", "Inloggen / Registreren");

        cy.get('._ticketSelector_1r6ku_1:first-child ._selectTicket_1r6ku_37').first().find("button").last().click();


        cy.get('._sideBar_13834_29 button.btn-success').first().click();

        cy.get(".modal").should("have.class", "show");
    });

    it("Tickets are in shoppingcart when user add when logged in", () => {

        // login
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#exampleInputEmail1").type("cypress@e2e.com");
        cy.get("#exampleInputPassword1").type("E2ETesting!");

        cy.get("button").click();

        cy.get("._signInContainer_11k3u_65 a").should("have.text", "Welkom, Cypress!");

        cy.get("#nav-item-shopping-cart").click();

        cy.get(".cart-item", { timeout: 10000 }).should("have.length", 0);

        // try to add ticket into shopping cart
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get('._ticketSelector_1r6ku_1:first-child ._selectTicket_1r6ku_37').first().find("button").last().click();

        cy.get('._sideBar_13834_29 button.btn-success').first().click();

        cy.get("#nav-item-shopping-cart").click();

        cy.get(".cart-item", { timeout: 10000 }).should("have.length", 1);
    });

    it("Could buy ticket and is visable in purachse overview", () => {

        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#exampleInputEmail1").type("cypress@e2e.com");
        cy.get("#exampleInputPassword1").type("E2ETesting!");

        cy.get("button").click();

        cy.get("._signInContainer_11k3u_65 a").should("have.text", "Welkom, Cypress!");

        cy.get("#nav-item-shopping-cart").click();

        cy.get(".cart-item", { timeout: 10000 }).should("have.length.above", 0);

        cy.get(".payment-button").click();

        cy.url().should('include', '/Payment-success');

        cy.get('body').contains('Aankoopnummer').then((text) => {
            const purchaseId = text.text().split('Aankoopnummer: ')[1].trim(); // Dit haalt de GUID uit de tekst

            // Gebruik de GUID direct in de test
            cy.log(purchaseId); // Je kunt de GUID loggen voor debugging of verdere verificatie in de test

            cy.get("._signInContainer_11k3u_65 a").click();
            cy.get("#profile-ticket").click();

            cy.get(".list-group-item h5").contains(purchaseId).should("exist");
        });
    });
});
