/// <reference types="cypress" />

describe("Usuário não logado", () => {
  it("Redireciona de Contatos para a página de login", () => {
    cy.visit("localhost:5000/Contato");

    cy.get(":nth-child(1) > .form-label").contains("Login");
    cy.get(":nth-child(2) > .form-label").contains("Senha");
  });

  it("Redireciona de Usuários para a página de login", () => {
    cy.visit("localhost:5000/Usuario");

    cy.get(":nth-child(1) > .form-label").contains("Login");
    cy.get(":nth-child(2) > .form-label").contains("Senha");
  });

  it("Redireciona de AlterarSenha para a página de login", () => {
    cy.visit("localhost:5000/AlterarSenha");

    cy.get(":nth-child(1) > .form-label").contains("Login");
    cy.get(":nth-child(2) > .form-label").contains("Senha");
  });
});
