using Bullion.util;
using Bullion.views;
using BullionLibrary;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Bullion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Account account = new Account();
        private Cipher cipher = new Cipher();
        private int counter = 0;

        /// <summary>
        /// Opens the MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets default values for binded labels when window is loaded.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = account;
            dateLabel.Content = DateTime.Now.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Opens a JSON file that contains a Bullion account.
        /// File dialog will open to this projects directory where the account file can be located.
        /// </summary>
        /// <remarks>
        /// <para>If the data imported for a property equals zero, then the content will be "0".</para>
        /// <para>If there is a selected item in the resourceComboBox, then the selection will be reset.</para>
        /// <para>If there is a value in the amountTextBox, then the text will be cleared.</para>
        /// </remarks>>
        private void Import_Data_Click(object sender, RoutedEventArgs e)
        {            
            ImportData idata = new ImportData();
            account = idata.Import(account, cipher);

            DataValidation data = new DataValidation();
            
            if (data.ValidateDouble(account.Savings)) { savingsAmountLabel.Content = account.Savings.ToString("N"); }
                else { savingsAmountLabel.Content = 0; }
            if (data.ValidateDouble(account.Cash)) { cashAmountLabel.Content = account.Cash.ToString("N"); }
                else { cashAmountLabel.Content = 0; }
            if (data.ValidateDouble(account.Crypto)) { cryptoAmountLabel.Content = account.Crypto.ToString("N"); }
                else { cryptoAmountLabel.Content = 0; }
            if (data.ValidateDouble(account.Savings + account.Cash)) { totalAmountLabel.Content = (account.Savings + account.Cash).ToString("N"); }
                else { totalAmountLabel.Content = 0; }

            DataContext = account;
        }

        /// <summary>
        /// Saves a JSON file that contains a Bullion account.
        /// File dialog will open to this project's directory where the account file can be saved.
        /// </summary>
        /// <remarks>
        /// <para>If the user does not have an account Id, then an account Id will be assigned.</para>
        /// <para>If the user does have an account Id, then their account Id will not be changed.</para>
        /// </remarks>
        private void Export_Data_Click(object sender, RoutedEventArgs e)
        {
            if (account.Id == 0 &&
                account.Savings  > 0 ||
                account.Cash > 0 ||
                account.Crypto > 0)
            {
                Random random = new Random();
                account.Id = random.Next(10000000, 99999999);
            }
            ExportData exdata = new ExportData();
            exdata.Export(account, cipher);
        }

        /// <summary>
        /// Displays the HelpWindow.
        /// </summary>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow help = new HelpWindow();
            help.ShowDialog();
        }

        /// <summary>
        /// Displays the AboutWindow.
        /// </summary>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        /// <summary>
        /// Closes the MainWindow and opens the SignInWindow.
        /// </summary>
        private void Exit()
        {
            Close();
        }

        /// <summary>
        /// Calls the SignOut method if the user clicks on the Sign Out button.
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// If the "Esc" key is pressed, then a MessageBox will open,
        /// and ask the user to confirm their sign out request.
        /// </summary>
        private void Exit_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                const string message = "Are you sure you want to exit?";
                const string caption = "Exit";
                MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) { Exit(); }
            }
        }

        /// <summary>
        /// Changes the color theme of the GUI to darker colors.
        /// </summary>
        private void DarkMode()
        {
            mainGrid.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(45, 45, 48));
            menuGrid.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(19, 22, 31));
            resourcesGrid.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(102, 102, 112));
            transactionStackPanel.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(102, 102, 112));
            foreach (Button button in menuGrid.Children.OfType<Button>())
            {
                button.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(102, 102, 112));
                button.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            }
            foreach (Label label in mainGrid.Children.OfType<Label>())
            {
                label.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            }
            foreach (Label label in resourcesGrid.Children.OfType<Label>())
            {
                label.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            }
            foreach (Label label in transactionsGrid.Children.OfType<Label>())
            {
                label.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            }
            darkModeButton.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            clearButton.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            depositButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(102, 102, 112));
            withdrawButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(102, 102, 112));
            amountTextBox.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(102, 102, 112));
            amountTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
        }

        /// <summary>
        /// Changes the color theme of the GUI to lighter colors.
        /// </summary>
        private void LightMode()
        {
            mainGrid.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(242, 242, 242));
            menuGrid.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 80, 114));
            resourcesGrid.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            transactionStackPanel.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            foreach (Button button in menuGrid.Children.OfType<Button>())
            {
                button.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                button.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            }
            foreach (Label label in mainGrid.Children.OfType<Label>())
            {
                label.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            }
            foreach (Label label in resourcesGrid.Children.OfType<Label>())
            {
                label.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            }
            foreach (Label label in transactionsGrid.Children.OfType<Label>())
            {
                label.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            }
            darkModeButton.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            clearButton.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
            depositButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            withdrawButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            amountTextBox.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
            amountTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
        }

        /// <summary>
        /// The first darkModeButton click will always call DarkMode first.
        /// Afterwards, every other click will call LightMode, then DarkMode.
        /// </summary>
        /// <remarks>
        /// <para>The "counter" variable is initially set at 0.</para>
        /// <para>Every time darkModeButton is pressed, the counter will always be either 0 or 1.</para>
        /// <para>If counter = 0, then DarkMode is called.</para>
        /// <para>If counter = 1, then LightMode is called.</para>
        /// </remarks>
        private void DarkMode_Click(object sender, RoutedEventArgs e)
        {
            if (counter == 0)
            {
                DarkMode();
                darkModeButton.Content = "Light mode";
                counter++;
            }
            else
            {
                LightMode();
                darkModeButton.Content = "Dark mode";
                counter--;
            }
        }

        /// <summary>
        /// Returns the selected item from resourceComboBox.
        /// </summary>
        private string GetItem()
        {
            string item = ((ComboBoxItem)resourceComboBox.SelectedItem).Content.ToString();
            return item;
        }

        /// <summary>
        /// Allows the amountTextBox to only accept numbers 0-9 and "." characters.
        /// </summary>
        private void AmountTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);
        }

        /// <summary>
        /// Disables Copy, Cut, and Paste commands to prevent invalid input from entering the amountTextBox.
        /// </summary>
        private void AmountTextBox_PreviewExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (e.Command == System.Windows.Input.ApplicationCommands.Copy ||
                e.Command == System.Windows.Input.ApplicationCommands.Cut ||
                e.Command == System.Windows.Input.ApplicationCommands.Paste)
            { e.Handled = true; }
        }

        /// <summary>
        /// Checks if there is more than one period in amountTextBox.
        /// </summary>
        private static bool HasPeriods(String str, char c)
        {
            return str.Count(x => x == c) > 1;
        }

        /// <summary>
        /// If there are transactions listed in the transactionStackPanel, then the panel will be cleared.
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            resourceComboBox.SelectedIndex = -1;
            amountTextBox.Clear();

            int i = transactionStackPanel.Children.Count;

            if (i == 1) { return; }
            else if (i > 1) { transactionStackPanel.Children.RemoveRange(1, i); }
        }

        /// <summary>
        /// Updates the transactionStackPanel by adding a label to it.
        /// The information of the label is the amount deposited into the account.
        /// </summary>
        /// <remarks>
        /// <para>If there is no item selected in the resourceComboBox, then no label and separator will be added.</para>
        /// <para>If there is a space in the amountTextBox, then no label and separator wil be added.</para>
        /// <para>If there are more than one ".", then no label and separator wil be added.</para>
        /// <para>If there is no amount in the amountTextbox, then no label and separator will be added.</para>
        /// <para>If there is a zero for the amount in the amountTextBox, then no label and separator will be added.</para>
        /// <para>If the previous four comments are true, then the account values will not be updated.</para>
        /// </remarks>
        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            DataValidation data = new DataValidation();
            if (resourceComboBox.SelectedItem == null) { return; }
            if (data.ValidateSpace(amountTextBox.Text.ToString())) { return; }
            if (HasPeriods(amountTextBox.Text, "."[0]) == true) { return; }
            if (data.ValidateString(amountTextBox.Text.ToString())) { return; }
            else
            {
                double amount = double.Parse(amountTextBox.Text.ToString());
                if (data.ValidateDouble(amount))
                {
                    Transaction transaction = new Transaction();
                    string item = GetItem();

                    switch (item)
                    {
                        case "Savings":
                            savingsAmountLabel.Content = transaction.DepositSavings(account, amount).ToString("N");
                            totalAmountLabel.Content = (account.Savings + account.Cash).ToString("N");
                            if (savingsAmountLabel.Content.ToString() == "0.00") { savingsAmountLabel.Content = 0; }
                            if (totalAmountLabel.Content.ToString() == "0.00") { totalAmountLabel.Content = 0; }
                            break;
                        case "Cash":
                            cashAmountLabel.Content = transaction.DepositCash(account, amount).ToString("N");
                            totalAmountLabel.Content = (account.Savings + account.Cash).ToString("N");
                            if (cashAmountLabel.Content.ToString() == "0.00") { cashAmountLabel.Content = 0; }
                            if (totalAmountLabel.Content.ToString() == "0.00") { totalAmountLabel.Content = 0; }
                            break;
                        case "Crypto":
                            cryptoAmountLabel.Content = transaction.DepositCrypto(account, amount).ToString("N");
                            if (cryptoAmountLabel.Content.ToString() == "0.00") { cryptoAmountLabel.Content = 0; }
                            break;
                    }
                    CustomControl control = new CustomControl();

                    Label depositLabel = new Label();
                    depositLabel = control.DepositLabel(amount);
                    if (item.Equals("Savings")) { depositLabel.Content = depositLabel.Content + "  Savings"; }
                    if (item.Equals("Cash")) { depositLabel.Content = depositLabel.Content + "  Cash"; }
                    if (item.Equals("Crypto")) { depositLabel.Content = depositLabel.Content.ToString().Replace("$", "") + "  Crypto"; }

                    Separator separator = new Separator();
                    separator = control.LineSeparator();

                    transactionStackPanel.Children.Add(depositLabel);
                    transactionStackPanel.Children.Add(separator);
                }
            }
        }

        /// <summary>
        /// Updates the transactionStackPanel by adding a label to it.
        /// The information of the label is the amount withdrawn from the account.
        /// </summary>
        /// <remarks>
        /// <para>If there is no item selected in the resourceComboBox, then no label and separator will be added.</para>
        /// <para>If there is a space in the amountTextBox, then no label and separator wil be added.</para>
        /// <para>If there are more than one ".", then no label and separator wil be added.</para>
        /// <para>If there is no amount in the amountTextbox, then no label and separator will be added.</para>
        /// <para>If there is a zero for the amount in the amountTextBox, then no label and separator will be added.</para>
        /// <para>If the previous four comments are true, then the account values will not be updated.</para>
        /// </remarks>
        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            DataValidation data = new DataValidation();
            if (resourceComboBox.SelectedItem == null) { return; }
            if (data.ValidateSpace(amountTextBox.Text.ToString())) { return; }
            if (HasPeriods(amountTextBox.Text, "."[0]) == true) { return; }
            if (data.ValidateString(amountTextBox.Text.ToString())) { return; } 
            else
            {
                double amount = double.Parse(amountTextBox.Text.ToString());

                if (data.ValidateDouble(amount))
                {
                    Transaction transaction = new Transaction();
                    string item = GetItem();

                    if (item == "Savings" && amount > account.Savings ||
                        item == "Cash" && amount > account.Cash ||
                        item == "Crypto" && amount > account.Crypto)
                    {
                        InvalidAmountMessageBox();
                        return;
                    }

                        switch (item)
                    {
                        case "Savings":
                            savingsAmountLabel.Content = transaction.WithdrawSavings(account, amount).ToString("N");
                            totalAmountLabel.Content = (account.Savings + account.Cash).ToString("N");
                            if (savingsAmountLabel.Content.ToString() == "0.00") { savingsAmountLabel.Content = 0; }
                            if (totalAmountLabel.Content.ToString() == "0.00") { totalAmountLabel.Content = 0; }
                            break;
                        case "Cash":
                            cashAmountLabel.Content = transaction.WithdrawCash(account, amount).ToString("N");
                            totalAmountLabel.Content = (account.Savings + account.Cash).ToString("N");
                            if (cashAmountLabel.Content.ToString() == "0.00") { cashAmountLabel.Content = 0; }
                            if (totalAmountLabel.Content.ToString() == "0.00") { totalAmountLabel.Content = 0; }
                            break;
                        case "Crypto":
                            cryptoAmountLabel.Content = transaction.WithdrawCrypto(account, amount).ToString("N");
                            if (cryptoAmountLabel.Content.ToString() == "0.00") { cryptoAmountLabel.Content = 0; }
                            break;
                    }
                    CustomControl control = new CustomControl();

                    Label withdrawLabel = new Label();
                    withdrawLabel = control.WithdrawLabel(amount);
                    if (item.Equals("Savings")) { withdrawLabel.Content = withdrawLabel.Content + "  Savings"; }
                    if (item.Equals("Cash")) { withdrawLabel.Content = withdrawLabel.Content + "  Cash"; }
                    if (item.Equals("Crypto")) { withdrawLabel.Content = withdrawLabel.Content.ToString().Replace("$", "") + "  Crypto"; }
                    
                    Separator separator = new Separator();
                    separator = control.LineSeparator();

                    transactionStackPanel.Children.Add(withdrawLabel);
                    transactionStackPanel.Children.Add(separator);
                }
            }      
        }

        /// <summary>
        /// 
        /// </summary>
        private void InvalidAmountMessageBox()
        {
            const string message = "You cannot have a negative amount.";
            const string caption = "Invalid amount";
            MessageBox.Show(message, caption);
        }
    }
}
