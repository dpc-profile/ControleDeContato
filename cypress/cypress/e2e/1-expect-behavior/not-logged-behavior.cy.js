/// <reference types="cypress" />

describe("Usuário não logado", () => {
  it("Redireciona de Contatos para a página de login", () => {
    cy.visit("Contato");

    cy.get('[data-cy="login"]');
    cy.get('[data-cy="senha"]');
  });

  it("Redireciona de Usuários para a página de login", () => {
    cy.visit("Usuario");

    cy.get('[data-cy="login"]');
    cy.get('[data-cy="senha"]');
  });

  it("Redireciona de AlterarSenha para a página de login", () => {
    cy.visit("AlterarSenha");

    cy.get('[data-cy="login"]');
    cy.get('[data-cy="senha"]');
  });
});
