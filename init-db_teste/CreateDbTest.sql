# create databases
CREATE DATABASE IF NOT EXISTS `db_teste`;

# Grant rights to user
GRANT ALL PRIVILEGES ON *.* TO 'user'@'%';