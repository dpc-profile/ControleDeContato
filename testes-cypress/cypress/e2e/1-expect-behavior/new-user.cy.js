/// <reference types="cypress" />

import { v4 as uuidv4 } from "uuid";

describe("Interação do novo usuário", () => {
  const randContate = uuidv4();

  const nome = "Cy Tester";
  const email = "cy@gmail.com" + randContate;
  const login = "cytester" + randContate;
  const senha = "cytester";
  const novaSenha = "teste";

  const nomeContato = "Caio";
  const emailContato = "caio@gmail.com";
  const celularContato = "11 91234-8762";

  const novoNome = "Caio Amaraldo";

  beforeEach(() => {
    cy.visit("/");
  });

  it("Criar um novo usuário", () => {
    cy.visit("/");

    cy.get('[data-cy="NovoUsuario"]').click();

    // Checar se é o perfil padrão
    cy.get('[data-cy="perfil"]').contains("Padrão");

    cy.get('[data-cy="nome"]').type(nome);
    cy.get('[data-cy="email"]').type(email);
    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="senha"]').type(senha);

    cy.get('[data-cy="submit"]').click();

    cy.get(".alert").contains("Usuário cadastrado com sucesso");

    cy.visit("/");

    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="senha"]').type(senha);
    cy.get('[data-cy="submit"]').click();

    cy.get('[data-cy="sair"]').should("be.visible");
  });

  it("Criar um contato", () => {
    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="senha"]').type(senha);
    cy.get('[data-cy="submit"]').click();

    cy.get('[data-cy="sair"]').should("be.visible");

    cy.get('[data-cy="contato"]').click();

    cy.get(".display-4").contains("Listagem de contatos");

    cy.get('[data-cy="criar"]').click();

    cy.get('[data-cy="nome"]').type(nomeContato);
    cy.get('[data-cy="email"]').type(emailContato);
    cy.get('[data-cy="celular"]').type(celularContato);

    cy.get('[data-cy="submit"]').click();
    cy.get(".alert").contains("Contato cadastrado com sucesso");

    cy.get("#table-contatos tr:last").should("contain.text", nomeContato);
    cy.get("#table-contatos tr:last").should("contain.text", emailContato);
    cy.get("#table-contatos tr:last").should("contain.text", celularContato);
  });

  it("Editar um contato", () => {
    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="senha"]').type(senha);
    cy.get('[data-cy="submit"]').click();

    cy.get('[data-cy="sair"]').should("be.visible");
    cy.get('[data-cy="contato"]').click();
    cy.get(".display-4").contains("Listagem de contatos");

    cy.get("#table-contatos tr:last").contains(nomeContato);
    // Botão Editar
    cy.get("#table-contatos tr:last .btn-group > .btn-primary").click();

    cy.get('[data-cy="nome"]').clear().type(novoNome);

    // Botão alterar
    cy.get('[data-cy="submit"]').click();

    cy.get(".alert").contains("Contato atualizado com sucesso");

    cy.get("#table-contatos tr:last").contains(novoNome);
  });

  it("Apagar o contato", () => {
    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="senha"]').type(senha);
    cy.get('[data-cy="submit"]').click();

    cy.get('[data-cy="sair"]').should("be.visible");
    cy.get('[data-cy="contato"]').click();
    cy.get(".display-4").contains("Listagem de contatos");

    cy.get("#table-contatos tr:last").contains(novoNome);

    // Botão apagar
    cy.get("#table-contatos tr:last .btn-group > .btn-danger").click();

    // clica em confirmar exclusão
    cy.get('[data-cy="confirmar"]').click();

    cy.get(".alert").contains("Contato apagado com sucesso");

    cy.get("#table-contatos tr:last").should("not.contain.text", novoNome);
  });

  it("Alterar a senha do usuário", () => {
    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="senha"]').type(senha);
    cy.get('[data-cy="submit"]').click();

    cy.get('[data-cy="sair"]').should("be.visible");

    cy.get('[data-cy="alterarSenha"]').click();

    cy.get('[data-cy="senhaAtual"]').type(senha);
    cy.get('[data-cy="novaSenha"]').type(novaSenha);
    cy.get('[data-cy="cofirmarNovaSenha"]').type(novaSenha);
    cy.get('[data-cy="submit"]').click();
  });

  it("Usar nova senha para logar", () => {
    cy.visit("Login/Sair");

    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="senha"]').type(novaSenha);
    cy.get('[data-cy="submit"]').click();

    cy.get('[data-cy="sair"]').should("be.visible");
  });

  it("Apagar o usuário criado", () => {
    cy.visit("Login/Sair");

    cy.get('[data-cy="login"]').type("admin");
    cy.get('[data-cy="senha"]').type("admin");
    cy.get('[data-cy="submit"]').click();

    cy.get('[data-cy="sair"]').should("be.visible");

    cy.get('[data-cy="usuario"]').click();

    cy.get("#table-usuarios tr:last").contains(nome);

    // Botão apagar
    cy.get("#table-usuarios tr:last .btn-group > .btn-danger").click();

    // clica em confirmar exclusão
    cy.get('[data-cy="confirmar"]').click();

    cy.get(".alert").contains("Usuário apagado com sucesso");

    cy.get("#table-usuarios tr:last").should("not.contain.text", nome);
  });
});
