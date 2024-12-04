describe('Test shopping cart', () => {
    it('Should add ticket to shopping cart', () => {
        cy.login("cypress@e2e.com", "E2ETesting!");

        cy.get("#nav-item-shopping-cart").click();

        cy.get(".cart-item", { timeout: 10000 }).should("have.length", 0);

        // try to add ticket into shopping cart
        cy.visit('/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get('._eventInfo_13834_13 h1').then((text) => {
            const eventName = text.text();
            cy.get('._ticketSelectorContainer_13834_111 ._ticketSelector_1r6ku_1:first-child').as("TicketSelector").find('.d-flex > p').then((text) => {
                const ticketName = text.text();

                cy.get("@TicketSelector").find("._selectTicket_1r6ku_37").first().find("button").last().click();

                cy.get('._sideBar_13834_29 button.btn-success').first().click();

                cy.get("#nav-item-shopping-cart").click();

                cy.get(".cart-item", { timeout: 10000 }).should("have.length", 1);

                cy.get(".cart-item").first().find(".ticket-name #ticket-name").should("have.text", ticketName);
                cy.get(".cart-item").first().find(".ticket-name strong").should("have.text", eventName);
            });
        });
    });

    it('Should delete ticket in shopping cart', () => {
        cy.login("cypress@e2e.com", "E2ETesting!");

        cy.get("#nav-item-shopping-cart").click();

        cy.get(".cart-item", { timeout: 10000 }).should("have.length", 1);

        cy.get(".cart-item").first().find("button#btn-remove-item").click();

        cy.get(".cart-item", { timeout: 10000 }).should("have.length", 0);
    });

    it("Could buy ticket and is visable in purachse overview", () => {
        cy.login("cypress@e2e.com", "E2ETesting!");

        cy.get("#nav-item-shopping-cart").click();

        cy.get(".cart-item", { timeout: 10000 }).should("have.length", 0);

        cy.visit('/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get('._eventInfo_13834_13 h1').then((text) => {
            const eventName = text.text();
            cy.get('._ticketSelectorContainer_13834_111 ._ticketSelector_1r6ku_1:first-child').as("TicketSelector").find('.d-flex > p').then((text) => {
                const ticketName = text.text();

                cy.get("@TicketSelector").find("._selectTicket_1r6ku_37").first().find("button").last().click();

                cy.get('._sideBar_13834_29 button.btn-success').first().click();

                cy.get("#nav-item-shopping-cart").click();

                cy.get(".cart-item", { timeout: 10000 }).should("have.length", 1);

                cy.get(".cart-item").first().find(".ticket-name #ticket-name").should("have.text", ticketName);
                cy.get(".cart-item").first().find(".ticket-name strong").should("have.text", eventName);
            });
        });

        cy.get(".payment-button").click();

        cy.url().should('include', '/Payment-success');

        cy.get('body').contains('Aankoopnummer').then((text) => {
            const purchaseId = text.text().split('Aankoopnummer: ')[1].trim(); // Dit haalt de GUID uit de tekst

            cy.log(purchaseId);

            cy.get(".sign-in-container a").click();
            cy.get("#profile-ticket").click();

            cy.get(".list-group-item h5").contains(purchaseId).should("exist");
        });
    });
});