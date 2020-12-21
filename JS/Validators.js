// ------------------------------------------------------------------------------------------------------------------------
// Validators
// ------------------------------------------------------------------------------------------------------------------------
methods: {
			onEntityInitialized: function() {
				this.callParent(arguments);
			},
			// Переопределение базового метода, инициализирующего пользовательские валидаторы.
			setValidationConfig: function() {
				// Вызывает инициализацию валидаторов родительской модели представления.
				this.callParent(arguments);
				// Для колонки добавляется метод-валидатор dueDateValidator().
				this.addColumnValidator("StartDate", this.checkStartDate);
				this.addColumnValidator("EndDate", this.checkEdnDate);
			},
			checkStartDate: function(value, column) {
				let msg = "";
				if (value && this.$EndDate) {
					if (value > this.$EndDate) {
						msg = this.get("Resources.Strings.CheckDates");
					}
				}
				return {
					invalidMessage: msg
				};
			},
			checkEndDate: function(value, column) {
				let msg = "";
				if (value && this.$StartDate) {
					if (value < this.$StartDate) {
						msg = this.get("Resources.Strings.CheckDates");
					}
				}
				return {
					invalidMessage: msg
				};
			},
		},
