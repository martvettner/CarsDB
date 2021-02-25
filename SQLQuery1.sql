Select CarInGarage.CarModelName, Car.CarMarkName From Car
Join CarInGarage on CarInGarage.CarMarkId = Car.Id
Where Car.CarMarkName Like 'Tesla'