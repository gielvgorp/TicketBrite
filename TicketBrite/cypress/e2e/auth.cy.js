describe('Authentication register and login', () => {
    it('should switch between login and register', () => {
        // Bezoek de homepagina
        cy.visit('http://localhost:5173/authenticatie');

        cy.get('.text-secondary a').as('registerLink').should('exist');
        cy.get('@registerLink').should('have.text', 'Maak hier gratis een account aan!');

        cy.get('.col-8 h1').should('have.text', 'Inloggen'); // check if is on login page

        cy.get('@registerLink').click();

        cy.url().should("eq", "http://localhost:5173/authenticatie/register");

        cy.get('.text-secondary a').as('loginLink').should('exist');
        cy.get('@loginLink').should('have.text', 'Log hier in!');

        cy.get('.col-8 h1').should('have.text', 'Account aanmaken'); // check if is on regisger page

        cy.get('@loginLink').click();

        cy.url().should("eq", "http://localhost:5173/authenticatie");
    });

    it("Should show error message on not matching credentials", () => {
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#exampleInputEmail1").type("cypress@test.com");
        cy.get("#exampleInputPassword1").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Gebruiker niet gevonden");
    });

    it("Should show error message on password empty", () => {
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#exampleInputEmail1").type("cypress@test.com");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Gebruiker niet gevonden");
    })

    it("Should show error message on email empty", () => {
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#exampleInputPassword1").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Gebruiker niet gevonden");
    });

    it("Should show error message on register empty full name", () => {
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#exampleInputEmail1").type("cypress@test.nl");
        cy.get("#exampleInputPassword1").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Een of meerdere velden zijn leeg!");
    });

    it("Should show error message on register empty email empty", () => {
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#full-name-input").type("Cypress");
        cy.get("#exampleInputPassword1").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Een of meerdere velden zijn leeg!");
    });

    it("Should show error message on register empty password", () => {
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#full-name-input").type("Cypress");
        cy.get("#exampleInputEmail1").type("cypress@test.nl");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Een of meerdere velden zijn leeg!");
    });

    it("Should show error message on email already in use", () => {
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#full-name-input").type("Cypress");
        cy.get("#exampleInputEmail1").type("gielvg1@gmail.com");
        cy.get("#exampleInputPassword1").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Email adres bestaat al!");
    });

    it("Should login successfull", () => {
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#exampleInputEmail1").type("cypress@e2e.com");
        cy.get("#exampleInputPassword1").type("E2ETesting!");

        cy.get("button").click();

        cy.get("._signInContainer_11k3u_65 a").should("have.text", "Welkom, Cypress!");
    });
});
