CREATE TABLE IF NOT EXISTS Patients (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS Orders (
    Id SERIAL PRIMARY KEY,
    PatientId INT NOT NULL REFERENCES Patients(Id),
    Message VARCHAR(500) NULL
);

INSERT INTO Patients (Name)
VALUES ('小民'), ('小民2'), ('小民3'), ('小民4'), ('小民5');
