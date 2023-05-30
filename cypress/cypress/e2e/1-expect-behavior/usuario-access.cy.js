/// <reference types="cypress" />

describe("Acesso a página de usuários", () => {
  beforeEach(() => {
    cy.visit("http://localhost:5000/Login/Sair");
    cy.visit("http://localhost:5000");

    cy.get('[data-cy="login"]').type("sus");
    cy.get('[data-cy="senha"]').type("admin");

    cy.get('[data-cy="submit"]').click()
    cy.get(":nth-child(4) > .nav-link").should("be.visible");

    cy.get(":nth-child(3) > .nav-link").contains("Usuarios").click();
  });

  it("Verificando página de Usuários", () => {
    cy.get(".display-4").contains("Listagem de usuarios");
  });
});
