using System;
using System.Windows.Forms;

namespace doan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btsua.Enabled = false;
            btxoa.Enabled = false;
            bttailai.Enabled = false;
            bttim.Enabled = false;
            tbtim.Enabled = false;
            rbmamathang.Enabled = false;
            rbtenmathang.Enabled = false;
            rbnsx.Enabled = false;
            rbhsd.Enabled = false;
            rbnhasx.Enabled = false;
            rbloaihang.Enabled = false;
            btsua2.Enabled = false;
            btxoa2.Enabled = false;
            bttailai2.Enabled = false;
            bttim2.Enabled = false;
            tbtim2.Enabled = false;
            rbmaloaihang.Enabled = false;
            rbtenloaihang.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Kiểm tra số dòng của 2 danh sách, nếu không có dòng nào sẽ vô hiệu hóa các chức năng xóa, sửa, tìm kiếm
        private bool kiemtrasodong(DataGridView dgv)
        {
            if (dgv.RowCount == 0)
            {
                return false;
            }
            return true;
        }

        //Đánh số thứ tự tự động cho các dòng ở 2 danh sách khi có các thay đổi như nhập mới, xóa
        private void sothutu(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                dgv.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
        }

        //Kiểm tra thông tin đang nhập hoặc đang sửa có trùng với các dòng đang có trong danh sách hiện tại hoặc trùng với chính dòng đang chọn hay không
        private bool kiemtratrung(string[] danhsach, string text)
        {
            for (int i = 0; i < danhsach.Length ; i++)
            {
                if (danhsach[i] == text)
                {
                    return false;
                }   
            }
            return true;
        }

        //Kiểm tra tính hợp lệ của ngày tháng
        private bool kiemtrangaythang(string ngaythang)
        {
            DateTime ngaytam;
            if (DateTime.TryParse(ngaythang, out ngaytam))
            {
                return true;
            }
            return false;
        }

        //Hàm tìm kiếm
        private void tim(DataGridView dgv, int column, string text)
        {
            int j = 0;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv.Rows[i].Cells[column].Value.ToString().ToUpper().Contains(text.ToUpper()) == false)
                {
                    dgv.Rows[i].Visible = false;
                    j++;
                }
            }
            if (j == dgv.RowCount)
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.Rows[i].Visible = true;
                }
                MessageBox.Show("Không tìm thấy kết quả nào!", "Thông báo!");
            }
            else
            {
                MessageBox.Show("Tìm thấy " + (dgv.RowCount-j).ToString() + " kết quả!", "Thông báo!");
            }
        }

        //Kiểm tra xem thông tin ở ô loại hàng có đang tồn tại danh sách bên tab loại hàng không? Nếu không sẽ không cho nhập hoặc sửa.
        private bool kiemtracbb(ComboBox cbb, string text)
        {
            for (int i=0; i<cbb.Items.Count; i++)
            {
                if (text == cbb.Items[i].ToString())
                {
                    return true;
                }
            }
            return false;
        }




        //Nút tạo mới sheet quản lý mặt hàng
        private void bttaomoi_Click(object sender, EventArgs e)
        {
            if (kiemtrasodong(danhsachloaihang) == false)
            {
                MessageBox.Show("Bạn chưa nhập bất kỳ loại hàng nào!\nVui lòng khởi tạo loại hàng bên tab quản lý loại hàng trước!", "Thông báo!");
            }
            else
            {
                if (tbmamathang.Text == "" || tbtenmathang.Text == "" ||
                    tbnsx.Text == "" || tbhsd.Text == "" ||
                    tbnhasx.Text == "" || cbbloaihang.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin!", "Thông báo");
                }
                else
                {
                    if (kiemtrangaythang(tbnsx.Text) == false || kiemtrangaythang(tbhsd.Text) == false)
                    {
                        MessageBox.Show("Bạn đã nhập sai định dạng ngày tháng năm, vui lòng nhập theo định dạng dd/mm/yyyy!", "Nhập sai định dạng!");
                    }
                    else
                    {
                        if (kiemtracbb(cbbloaihang, cbbloaihang.Text) == false)
                        {
                            MessageBox.Show("Thông tin loại hàng không có trong danh sách loại hàng hiện tại.\nVui lòng cập nhật danh sách loại hàng trước!", "Thông báo");
                        }
                        else
                        {
                            string[] danhsachkiemtratrung = new string[danhsachmathang.RowCount];
                            for (int i = 0; i < danhsachkiemtratrung.Length; i++)
                            {
                                danhsachkiemtratrung[i] = danhsachmathang.Rows[i].Cells[1].Value.ToString().ToUpper() + danhsachmathang.Rows[i].Cells[2].Value.ToString().ToUpper() +
                                                          danhsachmathang.Rows[i].Cells[3].Value.ToString().ToUpper() + danhsachmathang.Rows[i].Cells[4].Value.ToString().ToUpper() +
                                                          danhsachmathang.Rows[i].Cells[5].Value.ToString().ToUpper() + danhsachmathang.Rows[i].Cells[6].Value.ToString().ToUpper();
                            }
                            string daytamthoi = tbmamathang.Text.ToUpper() + tbtenmathang.Text.ToUpper() + tbnsx.Text.ToUpper() + tbhsd.Text.ToUpper() + tbnhasx.Text.ToUpper() + cbbloaihang.Text.ToUpper();
                            if (kiemtratrung(danhsachkiemtratrung, daytamthoi) == false)
                            {
                                MessageBox.Show("Thông tin vừa nhập trùng với một mặt hàng đã tồn tại.\nVui lòng lại thông tin khác!", "Thông báo");
                            }
                            else
                            {
                                danhsachmathang.Rows.Add(new object[] { "", tbmamathang.Text.ToUpper(), tbtenmathang.Text, tbnsx.Text, tbhsd.Text, tbnhasx.Text, cbbloaihang.Text });
                                tbmamathang.ResetText();
                                tbtenmathang.ResetText();
                                tbnsx.ResetText();
                                tbhsd.ResetText();
                                tbnhasx.ResetText();
                                cbbloaihang.ResetText();
                                sothutu(danhsachmathang);
                                btsua.Enabled = true;
                                btxoa.Enabled = true;
                                bttailai.Enabled = true;
                                bttim.Enabled = true;
                                tbtim.Enabled = true;
                                rbmamathang.Enabled = true;
                                rbtenmathang.Enabled = true;
                                rbnsx.Enabled = true;
                                rbhsd.Enabled = true;
                                rbnhasx.Enabled = true;
                                rbloaihang.Enabled = true;
                            } 
                        }
                    }
                }
                for (int i = 0; i < danhsachmathang.RowCount; i++)
                {
                    danhsachmathang.Rows[i].Visible = true;
                }
                danhsachmathang.ClearSelection();
            }
        }

        //Nút sửa ở sheet quản lý mặt hàng
        private void btsua_Click(object sender, EventArgs e)
        {
            if (danhsachmathang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo");
            }
            else if (danhsachmathang.SelectedRows.Count != 1)
            {
                MessageBox.Show("Mỗi lần chỉ có thể sửa 1 dòng!", "Thông báo");
                danhsachmathang.ClearSelection();
            }
            else
            {
                if (MessageBox.Show("Có chắc chắn bạn muốn sửa măt hàng này??", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int r = danhsachmathang.CurrentCell.RowIndex;
                    if (tbmamathang.Text == "" || tbtenmathang.Text == "" || tbnsx.Text == "" || tbhsd.Text == "" || tbnhasx.Text == "" || cbbloaihang.Text == "")
                    {
                        MessageBox.Show("Bạn chưa nhập đủ thông tin!\nVui lòng nhập đủ thông tin!", "Thông báo");
                    }
                    else
                    {
                        if (kiemtrangaythang(tbnsx.Text) == false || kiemtrangaythang(tbhsd.Text) == false)
                        {
                            MessageBox.Show("Bạn đã nhập sai định dạng ngày tháng năm, vui lòng nhập theo định dạng dd/mm/yyyy!", "Nhập sai định dạng!");
                        }
                        else
                        {
                            if (kiemtracbb(cbbloaihang, cbbloaihang.Text) == false)
                            {
                                MessageBox.Show("Thông tin loại hàng không có trong danh sách loại hàng hiện tại.\nVui lòng cập nhật danh sách loại hàng trước!", "Thông báo");
                            }
                            else
                            {
                                string[] danhsachkiemtraxemdasuachua = new string[danhsachmathang.RowCount];
                                for (int i = 0; i < danhsachkiemtraxemdasuachua.Length; i++)
                                {
                                    danhsachkiemtraxemdasuachua[i] = danhsachmathang.Rows[i].Cells[1].Value.ToString().ToUpper() + danhsachmathang.Rows[i].Cells[2].Value.ToString().ToUpper() +
                                                                        danhsachmathang.Rows[i].Cells[3].Value.ToString().ToUpper() + danhsachmathang.Rows[i].Cells[4].Value.ToString().ToUpper() +
                                                                        danhsachmathang.Rows[i].Cells[5].Value.ToString().ToUpper() + danhsachmathang.Rows[i].Cells[6].Value.ToString().ToUpper();
                                }
                                string daytamthoi = tbmamathang.Text.ToUpper() + tbtenmathang.Text.ToUpper() + tbnsx.Text.ToUpper() + tbhsd.Text.ToUpper() + tbnhasx.Text.ToUpper() + cbbloaihang.Text.ToUpper();
                                if (kiemtratrung(danhsachkiemtraxemdasuachua, daytamthoi) == false)
                                {
                                    if (tbmamathang.Text.ToUpper() == danhsachmathang.Rows[r].Cells[1].Value.ToString().ToUpper() &&
                                        tbtenmathang.Text.ToUpper() == danhsachmathang.Rows[r].Cells[2].Value.ToString().ToUpper() &&
                                        tbnsx.Text.ToUpper() == danhsachmathang.Rows[r].Cells[3].Value.ToString().ToUpper() &&
                                        tbhsd.Text.ToUpper() == danhsachmathang.Rows[r].Cells[4].Value.ToString().ToUpper() &&
                                        tbnhasx.Text.ToUpper() == danhsachmathang.Rows[r].Cells[5].Value.ToString().ToUpper() &&
                                        cbbloaihang.Text.ToUpper() == danhsachmathang.Rows[r].Cells[6].Value.ToString().ToUpper())
                                    {
                                        MessageBox.Show("Thông tin đã nhập trùng với thông tin trước khi sửa!\nVui lòng cập nhật thông tin mới!", "Thông báo");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Thông tin vừa nhập trùng với thông tin của một mặt hàng đã tồn tại.\nVui lòng nhập lại thông tin mới!", "Thông báo");
                                    }
                                }
                                else
                                {
                                    danhsachmathang.Rows[r].Cells[1].Value = tbmamathang.Text.ToUpper();
                                    danhsachmathang.Rows[r].Cells[2].Value = tbtenmathang.Text;
                                    danhsachmathang.Rows[r].Cells[3].Value = tbnsx.Text;
                                    danhsachmathang.Rows[r].Cells[4].Value = tbhsd.Text;
                                    danhsachmathang.Rows[r].Cells[5].Value = tbnhasx.Text;
                                    danhsachmathang.Rows[r].Cells[6].Value = cbbloaihang.Text;
                                    MessageBox.Show("Đã sửa thành công!", "Thông báo");
                                    tbmamathang.ResetText();
                                    tbtenmathang.ResetText();
                                    tbnsx.ResetText();
                                    tbhsd.ResetText();
                                    tbnhasx.ResetText();
                                    cbbloaihang.ResetText();
                                    danhsachmathang.ClearSelection();
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < danhsachmathang.RowCount; i++)
                {
                    danhsachmathang.Rows[i].Visible = true;
                }
            }
        }

        //Nút xóa ở sheet quản lý mặt hàng
        private void btxoa_Click(object sender, EventArgs e)
        {
            if (danhsachmathang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo");
            }
            else
            {
                if (MessageBox.Show("Có chắc chắn bạn muốn xóa các loại hàng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in danhsachmathang.SelectedRows)
                    {
                        danhsachmathang.Rows.RemoveAt(row.Index);
                    }
                    sothutu(danhsachmathang);
                    for (int i = 0; i < danhsachmathang.RowCount; i++)
                    {
                        danhsachmathang.Rows[i].Visible = true;
                    }
                    if (kiemtrasodong(danhsachmathang) == false)
                    {
                        btsua.Enabled = false;
                        btxoa.Enabled = false;
                        bttailai.Enabled = false;
                        bttim.Enabled = false;
                        tbtim.Enabled = false;
                        rbmamathang.Enabled = false;
                        rbtenmathang.Enabled = false;
                        rbnsx.Enabled = false;
                        rbhsd.Enabled = false;
                        rbnhasx.Enabled = false;
                        rbloaihang.Enabled = false;
                    }
                }
            }
            danhsachmathang.ClearSelection();
        }

        //Nút tìm kiếm ở sheet quản lý mặt hàng
        private void bttim_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < danhsachmathang.RowCount; i++)
            {
                danhsachmathang.Rows[i].Visible = true;
            }
            if (tbtim.Text == "")
            {
                if (rbmamathang.Checked == false && rbtenmathang.Checked == false &&
                rbnsx.Checked == false && rbhsd.Checked == false &&
                rbnhasx.Checked == false && rbloaihang.Checked == false)
                {
                    MessageBox.Show("Vui lòng nhập từ khóa và chọn phạm vi tìm kiếm!", "Thông báo!");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập từ khóa!", "Thông báo!");
                }
            }
            else
            {
                if (rbmamathang.Checked == false && rbtenmathang.Checked == false &&
                rbnsx.Checked == false && rbhsd.Checked == false &&
                rbnhasx.Checked == false && rbloaihang.Checked == false)
                {
                    MessageBox.Show("Vui lòng chọn phạm vi tìm kiếm!", "Thông báo!");
                }
                else
                {
                    if (rbmamathang.Checked == true)
                    {
                        tim(danhsachmathang, 1, tbtim.Text);
                        rbmamathang.Checked = false;
                        tbtim.ResetText();
                    }
                    else if (rbtenmathang.Checked == true)
                    {
                        tim(danhsachmathang, 2, tbtim.Text);
                        rbtenmathang.Checked = false;
                        tbtim.ResetText();
                    }
                    else if (rbnsx.Checked == true)
                    {
                        tim(danhsachmathang, 3, tbtim.Text);
                        rbnsx.Checked = false;
                        tbtim.ResetText();
                    }
                    else if (rbhsd.Checked == true)
                    {
                        tim(danhsachmathang, 4, tbtim.Text);
                        rbhsd.Checked = false;
                        tbtim.ResetText();
                    }
                    else if (rbnhasx.Checked == true)
                    {
                        tim(danhsachmathang, 5, tbtim.Text);
                        rbnhasx.Checked = false;
                        tbtim.ResetText();
                    }
                    else
                    {
                        tim(danhsachmathang, 6, tbtim.Text);
                        rbloaihang.Checked = false;
                        tbtim.ResetText();
                    }
                }
            }
            danhsachmathang.ClearSelection();
        }
        
        //Nút refresh lại danh sách ở sheet quản lý mặt hàng
        private void bttailai_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < danhsachmathang.RowCount; i++)
            {
                danhsachmathang.Rows[i].Visible = true;
            }
            if (kiemtrasodong(danhsachmathang) == false)
            {
                btsua.Enabled = false;
                btxoa.Enabled = false;
                bttailai.Enabled = false;
                bttim.Enabled = false;
                tbtim.Enabled = false;
                rbmamathang.Enabled = false;
                rbtenmathang.Enabled = false;
                rbnsx.Enabled = false;
                rbhsd.Enabled = false;
                rbnhasx.Enabled = false;
                rbloaihang.Enabled = false;
            }
            danhsachmathang.ClearSelection();
        }

        //Nút thoát ở sheet quản lý mặt hàng
        private void btthoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Có chắc chắn bạn muốn thoát khỏi ứng dụng?", "Thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        



  
        //Nút tạo mới ở sheet quản lý loại hàng
        private void bttaomoi2_Click(object sender, EventArgs e)
        {
            if (tbmaloaihang.Text == "" || tbtenloaihang.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin!", "Thông báo");
            }
            else
            {
                string[] danhsachkiemtramaloaihang = new string[danhsachloaihang.RowCount];
                string[] danhsachkiemtratenloaihang = new string[danhsachloaihang.RowCount];
                for (int i = 0; i < danhsachkiemtramaloaihang.Length; i++)
                {
                    danhsachkiemtramaloaihang[i] = danhsachloaihang.Rows[i].Cells[1].Value.ToString().ToUpper();
                }
                for (int i = 0; i < danhsachkiemtratenloaihang.Length; i++)
                {
                    danhsachkiemtratenloaihang[i] = danhsachloaihang.Rows[i].Cells[2].Value.ToString().ToUpper();
                }
                if (kiemtratrung(danhsachkiemtramaloaihang, tbmaloaihang.Text.ToUpper()) ==false)
                {
                    MessageBox.Show("Mã này đã được sử dụng cho loại hàng khác.\nVui lòng nhập mã khác!", "Thông báo");
                }
                else
                {
                    if (kiemtratrung(danhsachkiemtratenloaihang, tbtenloaihang.Text.ToUpper()) == false)
                    {
                        MessageBox.Show("Tên này đã được sử dụng cho mã loại hàng khác.\nVui lòng nhập tên khác!", "Thông báo");
                    }
                    else
                    {
                        danhsachloaihang.Rows.Add(new object[] { "", tbmaloaihang.Text.ToUpper(), tbtenloaihang.Text });
                        tbmaloaihang.ResetText();
                        tbtenloaihang.ResetText();
                        sothutu(danhsachloaihang);
                        btsua2.Enabled = true;
                        btxoa2.Enabled = true;
                        bttailai2.Enabled = true;
                        bttim2.Enabled = true;
                        tbtim2.Enabled = true;
                        rbmaloaihang.Enabled = true;
                        rbtenloaihang.Enabled = true;
                        cbbloaihang.Items.Clear();
                        for (int i = 0; i < danhsachloaihang.RowCount; i++)
                        {
                            cbbloaihang.Items.Add(danhsachloaihang.Rows[i].Cells[1].Value.ToString() + " | " + danhsachloaihang.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }
            for (int i = 0; i < danhsachloaihang.RowCount; i++)
            {
                danhsachloaihang.Rows[i].Visible = true;
            }
            danhsachloaihang.ClearSelection();
        }

        //Nút sửa ở sheet quản lý loại hàng
        private void btsua2_Click(object sender, EventArgs e)
        {
            if (danhsachloaihang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo");
            }
            else if (danhsachloaihang.SelectedRows.Count != 1)
            {
                MessageBox.Show("Mỗi lần chỉ có thể sửa 1 dòng!", "Thông báo");
                danhsachloaihang.ClearSelection();
            }
            else
            {
                if (MessageBox.Show("Có chắc chắn bạn muốn sửa loại hàng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int r = danhsachloaihang.CurrentCell.RowIndex;
                    if (tbmaloaihang.Text == "" || tbtenloaihang.Text == "")
                    {
                        MessageBox.Show("Bạn chưa nhập đủ thông tin!\nVui lòng nhập đủ thông tin!", "Thông báo");
                    }
                    else
                    {
                        string[] danhsachkiemtraxemdasuahaychua2 = new string[danhsachloaihang.RowCount];
                        for (int i = 0; i < danhsachkiemtraxemdasuahaychua2.Length; i++)
                        {
                            danhsachkiemtraxemdasuahaychua2[i] = danhsachloaihang.Rows[i].Cells[1].Value.ToString().ToUpper() + danhsachloaihang.Rows[i].Cells[2].Value.ToString().ToUpper();
                        }
                        string daytamthoi2 = tbmaloaihang.Text.ToUpper() + tbtenloaihang.Text.ToUpper();
                        if (kiemtratrung(danhsachkiemtraxemdasuahaychua2, daytamthoi2) == false)
                        {
                            if (tbmaloaihang.Text.ToUpper() == danhsachloaihang.Rows[r].Cells[1].Value.ToString().ToUpper() && tbtenloaihang.Text.ToUpper() == danhsachloaihang.Rows[r].Cells[2].Value.ToString().ToUpper())
                            {
                                MessageBox.Show("Thông tin bạn vừa nhập trùng với thông tin trước khi sửa!\nVui lòng nhập lại thông tin mới!", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Thông tin bạn vừa nhập trùng với thông tin của một loại hàng đã tồn tại.\nVui lòng nhập lại thông tin mới!", "Thông báo");
                            }
                        }
                        else
                        {
                            string[] danhsachkiemtramaloaihang = new string[danhsachloaihang.RowCount - 1];
                            string[] danhsachkiemtratenloaihang = new string[danhsachloaihang.RowCount - 1];
                            int k = 0;
                            int p = 0;
                            while (k < danhsachloaihang.RowCount)
                            {
                                if (k != r)
                                {
                                    danhsachkiemtramaloaihang[p] = danhsachloaihang.Rows[k].Cells[1].Value.ToString().ToUpper();
                                    danhsachkiemtratenloaihang[p] = danhsachloaihang.Rows[k].Cells[2].Value.ToString().ToUpper();
                                    p++;
                                }
                                k++;
                            }
                            if (kiemtratrung(danhsachkiemtramaloaihang, tbmaloaihang.Text.ToUpper()) == false)
                            {
                                MessageBox.Show("Mã này đã được sử dụng cho loại hàng khác.\nVui lòng nhập mã khác!", "Thông báo");
                            }
                            else
                            {
                                if (kiemtratrung(danhsachkiemtratenloaihang, tbtenloaihang.Text.ToUpper()) == false)
                                {
                                    MessageBox.Show("Tên này đã được sử dụng cho mã loại hàng khác.\nVui lòng nhập tên khác!", "Thông báo");
                                }
                                else
                                {
                                    danhsachloaihang.Rows[r].Cells[1].Value = tbmaloaihang.Text.ToUpper();
                                    danhsachloaihang.Rows[r].Cells[2].Value = tbtenloaihang.Text;
                                    MessageBox.Show("Đã sửa thành công!", "Thông báo");
                                    tbmaloaihang.ResetText();
                                    tbtenloaihang.ResetText();
                                    danhsachloaihang.ClearSelection();
                                    cbbloaihang.Items.Clear();
                                    for (int i = 0; i < danhsachloaihang.RowCount; i++)
                                    {
                                        cbbloaihang.Items.Add(danhsachloaihang.Rows[i].Cells[1].Value.ToString() + " | " + danhsachloaihang.Rows[i].Cells[2].Value.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < danhsachloaihang.RowCount; i++)
            {
                danhsachloaihang.Rows[i].Visible = true;
            }
        }

        //Nút xóa ở sheet quản lý loại hàng
        private void btxoa2_Click(object sender, EventArgs e)
        {
            if (danhsachloaihang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo");
            }
            else
            {
                if (MessageBox.Show("Có chắc chắn bạn muốn xóa các loại hàng này?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in danhsachloaihang.SelectedRows)
                    {
                        danhsachloaihang.Rows.RemoveAt(row.Index);
                    }
                    sothutu(danhsachloaihang);
                    for (int i = 0; i < danhsachloaihang.RowCount; i++)
                    {
                        danhsachloaihang.Rows[i].Visible = true;
                    }
                    if (kiemtrasodong(danhsachloaihang) == false)
                    {
                        btsua2.Enabled = false;
                        btxoa2.Enabled = false;
                        bttailai2.Enabled = false;
                        bttim2.Enabled = false;
                        tbtim2.Enabled = false;
                        rbmaloaihang.Enabled = false;
                        rbtenloaihang.Enabled = false;
                    }
                    cbbloaihang.Items.Clear();
                    for (int i = 0; i < danhsachloaihang.RowCount; i++)
                    {
                        cbbloaihang.Items.Add(danhsachloaihang.Rows[i].Cells[1].Value.ToString() + " | " + danhsachloaihang.Rows[i].Cells[2].Value.ToString());
                    }
                }
            }
            danhsachloaihang.ClearSelection();
        }

        //Nút tìm kiếm ở sheet quản lý loại hàng
        private void bttim2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < danhsachloaihang.RowCount; i++)
            {
                danhsachloaihang.Rows[i].Visible = true;
            }
            if (tbtim2.Text == "")
            {
                if (rbmaloaihang.Checked == false && rbtenloaihang.Checked == false)
                {
                    MessageBox.Show("Vui lòng nhập từ khóa và chọn phạm vi tìm kiếm!", "Thông báo!");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập từ khóa!", "Thông báo!");
                }
            }
            else
            {
                if (rbmaloaihang.Checked == false && rbtenloaihang.Checked == false)
                {
                    MessageBox.Show("Vui lòng chọn phạm vi tìm kiếm!", "Thông báo!");
                }
                else
                {
                    if (rbmaloaihang.Checked == true)
                    {
                        tim(danhsachloaihang, 1, tbtim2.Text);
                        rbmaloaihang.Checked = false;
                        tbtim2.ResetText();
                    }
                    if (rbtenloaihang.Checked == true)
                    {
                        tim(danhsachloaihang, 2, tbtim2.Text);
                        rbtenloaihang.Checked = false;
                        tbtim2.ResetText();
                    }
                    danhsachloaihang.ClearSelection();
                }
            }
        }

        //Thao tác refresh lại danh sách ở sheet quản lý loại hàng
        private void bttailai2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < danhsachloaihang.RowCount; i++)
            {
                danhsachloaihang.Rows[i].Visible = true;
            }
            if (kiemtrasodong(danhsachloaihang) == false)
            {
                btsua2.Enabled = false;
                btxoa2.Enabled = false;
                bttailai2.Enabled = false;
                bttim2.Enabled = false;
                tbtim2.Enabled = false;
                rbmaloaihang.Enabled = false;
                rbtenloaihang.Enabled = false;
            }
            danhsachloaihang.ClearSelection();
        }

        //Nút thoát ở sheet quản lý loại hàng
        private void btthoat2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Có chắc chắn bạn muốn thoát khỏi ứng dụng?", "Thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        
    }
}

