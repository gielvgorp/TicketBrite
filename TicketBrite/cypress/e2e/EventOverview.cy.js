describe('Events overview page', () => {
    it('category should change after select change', () => {
        // Bezoek de homepagina
        cy.visit('http://localhost:5173/events');

        cy.get('#category').as("SelectBox").should('exist');
        cy.get("@SelectBox").select("muziek");

        cy.url().should("equal", "http://localhost:5173/events/muziek");
    });
});
